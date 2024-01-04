namespace InMa.Core.Types;

public class ServiceActionResult<T>
{
    private readonly string _errorMessage;
    private readonly T? _success;

    public ServiceActionResult(T success)
    {
        _errorMessage = string.Empty;
        _success = success;
        IsSuccess = true;
    }

    public ServiceActionResult(string errorMessage)
    {
        _success = default;
        _errorMessage = errorMessage;
        IsSuccess = false;
    }

    public T? GetResult() => _success;
    public string GetError() => _errorMessage;

    public bool IsSuccess { get; }

    public static explicit operator ServiceActionResult<T>(T success) => new(success);
    public static explicit operator ServiceActionResult<T>(string errorMessage) => new(errorMessage);
}
public class ServiceActionResult
{
    private readonly string _errorMessage;

    public ServiceActionResult()
    {
        _errorMessage = string.Empty;
        IsSuccess = true;
    }

    public ServiceActionResult(string errorMessage)
    {
        _errorMessage = errorMessage;
        IsSuccess = false;
    }

    public string GetError() => _errorMessage;

    public bool IsSuccess { get; }

    public static explicit operator ServiceActionResult(string errorMessage) => new(errorMessage);
}