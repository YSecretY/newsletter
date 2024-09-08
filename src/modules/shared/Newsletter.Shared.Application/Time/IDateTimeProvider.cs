namespace Newsletter.Shared.Application.Time;

public interface IDateTimeProvider
{
    public DateTime CurrentTime { get; }
}