pragma solidity ^0.8.10;

import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/security/ReentrancyGuard.sol";
import "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import "@openzeppelin/contracts/token/ERC20/utils/SafeERC20.sol";
import "@openzeppelin/contracts/utils/math/Math.sol";
import "@openzeppelin/contracts/utils/math/SafeMath.sol";


// Launchpad Smart Contract with Funds Distribution and Token Vesting functionality

contract ProLaunchpad is Ownable, ReentrancyGuard {
    using SafeMath for uint256;
    using SafeERC20 for IERC20;

    struct Vesting {
        uint256 balance;        // total amount of tokens to be released at the end of the vesting
        uint256 released;       // amount of tokens released
        bool revoked;           // whether or not the vesting has been revoked
        bool initialized;       // initialization flag of vesting identifier
    }

    event PurchaseTokens(address beneficiary, uint256 tokenAmount, uint256 busdAmount);
    event VestingCreated(address indexed beneficiary, uint256 amount);
    event ClaimedTokens(address indexed beneficiary, uint256 amountClaimed);
    event RevokedVesting(address beneficiary, uint256 vestedAmount);

    // BUSD in BNB Mainnet: https://bscscan.com/address/0xe9e7cea3dedca5984780bafc599bd69add087d56
    // BUSD in BNB Testnet: https://testnet.bscscan.com/address/0xcf1aecc287027f797b99650b1e020ffa0fb0e248
    IERC20 public BUSD;                     // address of the token to be accepted as payment for claiming the launched token
    IERC20 public token;                    // address of the token to be launched

    uint256 public immutable hardCap;       // max cap in BUSD
    uint256 public immutable softCap;       // min cap in BUSD
    uint256 public immutable saleStartTime; // sale start time 
    uint256 public immutable saleEndTime;   // sale end time

    uint256 public totalClaimableAmount;    // total amount of launched tokens to be claimable by others
    uint256 public totalPurchased;          // total amount of tokens already purchased
    uint256 public totalBUSDReceived;       // total amount of raised payment token
    uint256 public price;                   // price represents amount of tokens required to pay (paymentToken-BUSD) per token claimed (token)

    // Durations and timestamps are expressed in UNIX time, the same units as block.timestamp.
    uint256 public start;                   // start time of the vesting period
    uint256 public cliff;                   // cliff duration in seconds of the cliff in which tokens will begin to vest
    uint256 public duration;                // duration in seconds of the period in which the tokens will vest
    uint256 public slicePeriodSeconds;      // duration of a slice period for the vesting in seconds
    bool public revocable;                  // whether or not the vesting is revocable

    mapping (address => Vesting) private userVesting;
    address[] private vestings;

    uint256 public vestingTotalAmount;      // total amount of vested tokens


    // Limit executions to uninitalized launchpad state only
    modifier onlyUninitialized() {
        require(address(token) == address(0), "PL: the launchpad can be inititalized only once");
        _;
    }

    // Limit executions to initalized launchpad state only
    modifier onlyInitialized() {
        require(totalPurchased > 0, "PL: the launchpad had not been initialized yet");
        _;
    }

    // Limit executions to unstarted launchpad state only
    modifier onlyIfSaleUnstarted() {
        require(saleEndTime == 0, "PL: the launchpad can be started only once");
        _;
    }

    // Limit executions to launchpad in progress only
    modifier onlyIfSaleInProgress() {
        require(saleEndTime > 0, "PL: the launchpad has not been started yet");
        require(saleEndTime > block.timestamp, "PL: the launchpad has been finished");
        _;
    }

    // Limit executions to launchpad ended state only
    modifier onlyIfSaleEnded() {
        require(saleEndTime > 0, "PL: the launchpad has not been started yet");
        require(saleEndTime < block.timestamp, "PL: the launchpad has not ended yet");
        _;
    }


    modifier onlyIfVestingExists(address _beneficiary) {
        require(userVesting[_beneficiary].initialized == true);
        _;
    }

    modifier onlyIfVestingNotRevoked(address _beneficiary) {
        require(userVesting[_beneficiary].initialized == true);
        require(userVesting[_beneficiary].revoked == false);
        _;
    }


    constructor(IERC20 _BUSD, IERC20 _launchedToken, uint256 _hardCap, uint256 _softCap, uint256 _saleStartTime, uint256 _saleEndTime) {
        require(address(_BUSD) != address(0), "PL: zero address is not allowed");
        require(address(_launchedToken) != address(0), "PL: zero address is not allowed");
        require(_hardCap >= _softCap, "");
        require(_saleStartTime > block.timestamp, "");
        require(_saleStartTime < _saleEndTime, "");
        require(_saleEndTime < (block.timestamp + 12 weeks), "PL: endTime needs to be less than 12 weeks in the future");

        BUSD = _BUSD;
        token = _launchedToken;
        hardCap = _hardCap;
        softCap = _softCap;
        saleStartTime = _saleStartTime;
        saleEndTime = _saleEndTime;
    }


    // Starts the purchase tokens process. Should be reworked with chainlink keeper
    function startPurchase(uint256 _price) external onlyOwner onlyInitialized onlyIfSaleUnstarted returns (bool) {
        require(_price > 0, "PL: price should not be zero");
        price = _price;
        totalClaimableAmount = token.balanceOf(address(this));
        require(totalClaimableAmount > 0, "PL: the launchpad should be initialized with claimable tokens");
        return true;
    }

    function _verifyAllowance(address _allower, uint256 _amount, IERC20 _token) internal view returns (bool) {
        // Make sure the allower has provided the right allowance.
        uint256 ourAllowance = _token.allowance(_allower, address(this));
        require(_amount <= ourAllowance, "PL: make sure to add enought allowance");
        return true;
    }

    // Purchase tokens.
    function purchaseTokens(address _beneficiary, uint256 _amount) external nonReentrant {
        require(currentTime() >= saleStartTime, "PL: the sale is not started yet");
        require(currentTime() < saleEndTime, "PL: the sale is closed");

        // whitelisting

        uint256 busdAmount = _amount * price / (10**18);

        _verifyAllowance(_msgSender(), busdAmount, BUSD);
        require(totalBUSDReceived + busdAmount <= hardCap, "PL: purchase would exceed map cap");

        BUSD.safeTransferFrom(_msgSender(), address(this), busdAmount);

        _createVesting(_beneficiary, _amount); // move to separate txn and call by owner in case reached at least softCap - TBC

        totalPurchased += _amount;
        totalBUSDReceived += busdAmount;

        require(totalPurchased <= totalClaimableAmount, "PL: purchasing attempt exceeds totalClaimableAmount amount");

        emit PurchaseTokens(_beneficiary, _amount, busdAmount);
    }



    modifier enableVesting() {
        require(totalBUSDReceived <= hardCap, "PL: totalBUSDReceived should be <= hardCap");
        require(totalBUSDReceived >= softCap, "PL: totalBUSDReceived should be >= softCap");
        _;
    }


    // Set the token vesting schedule for launched token
    function setVesting(uint256 _start, uint256 _cliff, uint256 _duration, uint256 _slicePeriodSeconds, bool _revocable) external enableVesting onlyOwner {
        require(_start >= block.timestamp, "PL: start vesting needs to be in the future");
        require(_cliff <= _duration, "PL: cliff > duration");
        require(_duration > 0, "PL: duration must be > 0");
        require(_slicePeriodSeconds >= 1, "TV: slicePeriodSeconds must be >= 1");

        start = _start;
        cliff = _start.add(_cliff);
        duration = _duration;
        slicePeriodSeconds = _slicePeriodSeconds;
        revocable = _revocable;
    }

    // Creates a new vesting for a beneficiary
    function _createVesting(address _beneficiary, uint256 _amount) internal {
        require(_amount > 0, "PL: amount must be > 0");

        Vesting memory vesting = userVesting[_beneficiary];

        if (vesting.balance == 0) {
            // Create new record
            vesting = Vesting({
            balance: _amount,
            released: uint256(0),
            revoked: false,
            initialized: true
            });

            userVesting[_beneficiary] = vesting;

            vestings.push(_beneficiary);
        } else {
            // Add to existing record
            vesting.balance = uint256(vesting.balance.add(_amount));
        }

        vestingTotalAmount = vestingTotalAmount.add(_amount); // totalPurchased

        emit VestingCreated(_beneficiary, _amount);
    }


    // Releases claim for the launched token to the beneficiary
    function claimVestedTokens(address _beneficiary, uint256 _amount) public nonReentrant onlyIfVestingNotRevoked(_beneficiary) {
        Vesting storage vesting = userVesting[_beneficiary];
        bool isBeneficiary = _msgSender() == _beneficiary;
        bool isOwner = _msgSender() == owner();
        require(isBeneficiary || isOwner, "PL: only beneficiary and owner can release vested tokens");

        uint256 vestedAmount = _computeReleasableAmount(vesting);
        require(vestedAmount >= _amount, "PL: cannot release tokens, not enough vested tokens");

        vesting.released = vesting.released.add(_amount);
        vestingTotalAmount = vestingTotalAmount.sub(_amount);
        token.safeTransfer(_beneficiary, _amount);

        emit ClaimedTokens(_beneficiary, _amount);
    }

    // Computes the releasable amount of tokens for a particular vesting.
    function _computeReleasableAmount(Vesting memory _vesting) internal view returns (uint256) {
        uint256 _currentTime = currentTime();

        if ((_currentTime < cliff) || _vesting.revoked == true) {
            return 0;
        } else if (_currentTime >= start.add(duration)) {
            return _vesting.balance.sub(_vesting.released);
        } else {
            uint256 timeFromStart = _currentTime.sub(start);
            uint256 vestedSlicePeriods = timeFromStart.div(slicePeriodSeconds);
            uint256 vestedSeconds = vestedSlicePeriods.mul(slicePeriodSeconds);
            uint256 vestedAmount = _vesting.balance.mul(vestedSeconds).div(duration);
            vestedAmount = vestedAmount.sub(_vesting.released);
            return vestedAmount;
        }
    }

    function currentTime() internal view returns (uint256) {
        return block.timestamp;
    }

    // Revokes the vesting tokens for given beneficiary.
    function revoke(address _beneficiary) public onlyOwner onlyIfVestingNotRevoked(_beneficiary) {
        Vesting storage vesting = userVesting[_beneficiary];
        require(revocable == true, "TV: vesting is not revocable");

        uint256 vestedAmount = _computeReleasableAmount(vesting);
        if (vestedAmount > 0) {
            claimVestedTokens(_beneficiary, vestedAmount);
        }

        uint256 unreleased = vesting.balance.sub(vesting.released);
        vestingTotalAmount = vestingTotalAmount.sub(unreleased);
        vesting.revoked = true;

        emit RevokedVesting(_beneficiary, vestedAmount);
    }

    // Returns the vesting schedule structure information associated to a beneficiary
    function getVesting(address _beneficiary) public view returns (uint256, uint256, bool, bool) {
        Vesting memory vesting = userVesting[_beneficiary];
        return (vesting.balance, vesting.released, vesting.revoked, vesting.initialized);
    }

    // Returns the number of vestings
    function getVestingCount() public view returns (uint256) {
        return vestings.length;
    }

    address tokenFounder;   // address of owner of lauched token
    // uint256 totalTokens;    // amount of tokens that will be supplied to this contract
    event VestingFunded(uint256 totalTokens);

    // Function responsible for supplying tokens that will be vested
    function fundVesting(uint256 _totalTokens) external onlyOwner {
        _verifyAllowance(tokenFounder, _totalTokens, token);

        // totalTokens = _totalTokens;     // totalClaimableAmount
        token.transferFrom(tokenFounder, address(this), _totalTokens);
        emit VestingFunded(_totalTokens);
    }

    // Releases payment token.
    function withdrawFunds(address _recipient) external onlyIfSaleEnded onlyOwner nonReentrant returns (bool) {
        BUSD.safeTransfer(_recipient, BUSD.balanceOf(address(this)));
        return true;
    }

    // Releases unclaimed launched tokens back.
    function withdrawNotSoldTokens(address _recipient) external onlyIfSaleEnded onlyOwner nonReentrant returns (bool) {
        uint256 unclaimed = totalClaimableAmount - totalPurchased;
        token.safeTransfer(_recipient, unclaimed);
        totalClaimableAmount = 0;
        return true;
    }

}