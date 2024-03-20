namespace KahootBackend.Services.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() =>
        DateTime.Now;
}