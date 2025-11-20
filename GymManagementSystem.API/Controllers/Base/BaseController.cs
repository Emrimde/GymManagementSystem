using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController: ControllerBase
{
    protected ActionResult HandleResult<T>
       (Result<T> result)
    {
        return result.StatusCode == StatusCodeEnum.Ok ? Ok(result.Value)
            : result.StatusCode == StatusCodeEnum.NoContent ? NoContent()
            : !result.IsSuccess ?
            Problem(detail: result.ErrorMessage, statusCode: (int)result.StatusCode) : Ok(result.Value);
    }

    protected ActionResult HandleResult
       (ResultNoContent result)
    {
        return result.StatusCode == StatusCodeEnum.NoContent ? NoContent() : 
            Problem(detail: result.ErrorMessage, statusCode: (int)result.StatusCode);
    }

    protected ActionResult HandleListedResult<T>
    (Result<IEnumerable<T>> result)
    {
        return result.StatusCode == StatusCodeEnum.Ok ? Ok(result.Value)
            : result.StatusCode == StatusCodeEnum.NoContent ? NoContent()
            : !result.IsSuccess ?
            Problem(detail: result.ErrorMessage, statusCode: (int)result.StatusCode) : Ok(result.Value);
    }
}
