namespace Protagonist.Services;

public class ConverterService
{
    private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static long DateTimeToTimeStamp(DateTime dateTime)
    {
        var elapsedTime = dateTime - _epoch;
        return (long) elapsedTime.TotalSeconds;
    }
}
