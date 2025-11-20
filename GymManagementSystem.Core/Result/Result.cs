using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Result;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Value { get; set; }
    public string? ErrorMessage { get; set; }
    public StatusCodeEnum? StatusCode { get; set; }
    public static Result<T> Success(T value, StatusCodeEnum statusCode)
    {
        return new Result<T> { IsSuccess = true, Value = value, StatusCode = statusCode };
    }
    public static Result<T> Success(T value)
    {
        return new Result<T> { IsSuccess = true, Value = value };
    }
    public static Result<T> Failure(string errorMessage, StatusCodeEnum statusCode)
    {
        return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
    }
    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}