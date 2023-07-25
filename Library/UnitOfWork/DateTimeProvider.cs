namespace Library.UnitOfWork;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}


public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}