using Newsletter.Shared.Application.Time;

namespace Newsletter.Shared.Infrastructure.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime { get; } = DateTime.UtcNow;
}