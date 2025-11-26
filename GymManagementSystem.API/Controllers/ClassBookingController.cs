using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClassBooking;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassBookingResponse>>> GetAll(CancellationToken cancellationToken) => HandleListedResult(await _classBookingService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClassBookingDetailsResponse>> GetById([FromRoute] Guid id,CancellationToken cancellationToken) => HandleResult(await _classBookingService.GetByIdAsync(id,cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ClassBookingInfoResponse>> Create(ClassBookingAddRequest request) => HandleResult(await _classBookingService.CreateAsync(request));
}
