namespace Core.Common.Mediator.Exceptions;

[Serializable]
public sealed class ValidationException : ApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base("Validation Failure", "One or more validation errors occurred")
        => ErrorsDictionary =  new(() => errorsDictionary);

    public Lazy<IReadOnlyDictionary<string, string[]>> ErrorsDictionary { get; }
}