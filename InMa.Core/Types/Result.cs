namespace InMa.Core.Types;

public class Result<T>
{
    private readonly string _errorMessage;
    private readonly T? _success;

    public Result(T success)
    {
        _errorMessage = string.Empty;
        _success = success;
        IsSuccess = true;
    }

    public Result(string errorMessage)
    {
        _success = default;
        _errorMessage = errorMessage;
        IsSuccess = false;
    }

    public T? GetResult() => _success;
    public string GetError() => _errorMessage;

    public bool IsSuccess { get; }

    public static explicit operator Result<T>(T success) => new(success);
    public static explicit operator Result<T>(string errorMessage) => new(errorMessage);
}