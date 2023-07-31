namespace Core.Common.Mediator.Exceptions;


[Serializable]
public abstract class ApplicationException : Exception
{
    protected ApplicationException(string title, string message)
        : base(message) =>
        Title = title;

    public string Title { get; }
}
