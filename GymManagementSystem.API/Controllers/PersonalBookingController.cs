using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class PersonalBookingController : BaseController
{
    private readonly IPersonalBookingService _personalBookingService;
    public PersonalBookingController(IPersonalBookingService personalBookingService)
    {
        _personalBookingService = personalBookingService;
    }

    [Authorize(Roles = "Owner,Manager,Client,Receptionist")]
    [HttpGet("cancel/{id:guid}")]
    public async Task<ActionResult<bool>> DeletePersonalBooking([FromRoute] Guid id) => HandleResult(await _personalBookingService.DeletePersonalBooking(id));

    [Authorize(Roles = "Owner,Manager,Client,Receptionist")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonalBookingInfoResponse>> GetPersonalBooking([FromRoute] Guid id) => HandleResult(await _personalBookingService.GetPersonalBookingAsync(id));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("client-personal-bookings/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<PersonalBookingResponse>>> GetAllClientPersonalBookings([FromRoute] Guid clientId) => HandleListedResult(await _personalBookingService.GetAllClientPersonalBookings(clientId));

    [Authorize(Roles = "Owner,Manager,Client,Receptionist")]
    [HttpDelete("{personalBookingId:guid}")]
    public async Task<ActionResult> DeleteClientPersonalBooking([FromRoute] Guid personalBookingId) => HandleResult(await _personalBookingService.DeletePersonalBookingAsync(personalBookingId));

    [Authorize(Roles = "Client")]
    [HttpGet]
    public async Task<ActionResult<PersonalBookingInfoResponse>> GetWebAllPersonalBookingsByClientId() => HandleResult(await _personalBookingService.GetAllPersonalBookingsByClientIdAsync());

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("personal-booking-for-edit/{personalBookingId:guid}")]
    public async Task<ActionResult<PersonalBookingForEditResponse>> GetPersonalBookingForEdit([FromRoute] Guid personalBookingId) => HandleResult(await _personalBookingService.GetPersonalBookingForEditAsync(personalBookingId));

    [Authorize(Roles = "Owner,Manager,Client,Receptionist")]
    [HttpPost]
    public async Task<ActionResult<PersonalBookingInfoResponse>> CreatePersonalBooking([FromBody] PersonalBookingAddRequest entity) => HandleResult(await _personalBookingService.CreatePersonalBookingAsync(entity));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPut("pay-client/{id:guid}")]
    public async Task<ActionResult<PersonalBookingInfoResponse>> SetStatusToPaid(Guid id) => HandleResult(await _personalBookingService.SetStatusToPaidAsync(id));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPut("update-personal-booking/{personalBookingId:guid}")]
    public async Task<ActionResult> UpdatePersonalBooking([FromRoute] Guid personalBookingId, [FromBody] PersonalBookingUpdateRequest personalBookingUpdateRequest) => HandleResult(await _personalBookingService.UpdatePersonalBookingAsync(personalBookingId, personalBookingUpdateRequest));
    
}
