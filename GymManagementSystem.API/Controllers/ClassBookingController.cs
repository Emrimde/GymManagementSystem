using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class ClassBookingController : BaseController
{
    private readonly IClassBookingService _classBookingService;
    public ClassBookingController(IClassBookingService classBookingService)
    {
        _classBookingService = classBookingService;
    }

    [Authorize(Roles = "Receptionist,Manager,Owner")]
    [HttpGet("getAll/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<ClassBookingResponse>>> GetAllByClientId([FromRoute] Guid clientId) => HandleListedResult(await _classBookingService.GetAllByClientIdAsync(clientId));

    [Authorize(Roles = "Client")]
    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<ClassBookingResponse>>> GetAllClientReservatedClasses() => HandleListedResult(await _classBookingService.GetAllByClientIdAsync(null));

    [Authorize(Roles = "Receptionist,Manager,Owner,Client")]
    [HttpPost]
    public async Task<ActionResult<ClassBookingInfoResponse>> Create(ClassBookingAddRequest request) => HandleResult(await _classBookingService.CreateAsync(request));


    [Authorize(Roles = "Receptionist,Manager,Owner,Client")]
    [HttpDelete("{classBookingId:guid}")]
    public async Task<ActionResult<Unit>> DeleteClassBooking([FromRoute] Guid classBookingId) => HandleResult(await _classBookingService.DeleteClassBookingAsync(classBookingId));
}
