namespace PraPdBL_Backend.Common;

public static class TimeZoneHelper
{
    private const string WindowsWibId = "SE Asia Standard Time";
    private const string LinuxWibId = "Asia/Jakarta";

    public static TimeZoneInfo WibTimeZone { get; } = ResolveWibTimeZone();

    public static DateTime NowWib()
    {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, WibTimeZone);
    }

    private static TimeZoneInfo ResolveWibTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(WindowsWibId);
        }
        catch (TimeZoneNotFoundException)
        {
        }
        catch (InvalidTimeZoneException)
        {
        }

        return TimeZoneInfo.FindSystemTimeZoneById(LinuxWibId);
    }
}
