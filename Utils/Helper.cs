using System.Globalization;

namespace DiscordWebhookManager.Utils;

public static class Helper {

    public static string DateTimeToIso(DateTime dateTime) {
        return DateTimeToIso(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
    }

    private static string DateTimeToIso(int year, int month, int day, int hour, int minute, int second) {
        return new DateTime(year, month, day, hour, minute, second, 0, DateTimeKind.Local).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture);
    }
    
}