using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Result;

public class ResultNoContent
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public StatusCodeEnum? StatusCode { get; set; }
    public static ResultNoContent Success()
    {
        return new ResultNoContent { IsSuccess = true, StatusCode = StatusCodeEnum.NoContent };
    }
    public static ResultNoContent Failure(StatusCodeEnum statusCode) { return new ResultNoContent { IsSuccess = false, StatusCode = statusCode }; }
}

