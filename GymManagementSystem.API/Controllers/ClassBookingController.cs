using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class ClassBookingController : BaseController
{
    private readonly IClassBookingService _classBookingService;
    public ClassBookingController(IClassBookingService classBookingService)
    {
        _classBookingService = classBookingService;
    }

    [HttpGet("getAll/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<ClassBookingResponse>>> GetAllByClientId([FromRoute] Guid clientId) => HandleListedResult(await _classBookingService.GetAllByClientIdAsync(clientId));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClassBookingDetailsResponse>> GetById([FromRoute] Guid id) => HandleResult(await _classBookingService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ClassBookingInfoResponse>> Create(ClassBookingAddRequest request) => HandleResult(await _classBookingService.CreateAsync(request));
}
