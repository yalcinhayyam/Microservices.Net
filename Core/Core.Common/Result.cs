namespace Core.Common;

public class Result<T>
{
    private T? value;
    private Error? error;

    public static Result<T> Create(T value)
    {
        return new()
        {
            value = value
        };
    }

    public static Result<T> FromError(Error error)
    {
        return new() { error = error };
    }

    public static implicit operator Result<T>(T value)=> Create(value);
    public static implicit operator Result<T>(Error error)=> FromError(error);

    public bool IsSuccess => value != null;
    public T Value => IsSuccess ? value! : throw new Exception($"The result value has not a value {typeof(T).Name} {error!.GetType().Name}");
    public Error Error => !IsSuccess ? error! : throw new Exception($"The result has not a error {typeof(T).Name}");
}


public abstract record Error(string? Message = null) { }
public abstract record BusinessError(string? Message = null) : Error(Message) { }
public record NetworkError(string? Message = null) : Error(Message) { }