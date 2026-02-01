using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class PersonalBookingController : BaseController
{
    private readonly IPersonalBookingService _personalBookingService;
    public PersonalBookingController(IPersonalBookingService personalBookingService)
    {
        _personalBookingService = personalBookingService;
    }

    [HttpGet("cancel/{id:guid}")]
    public async Task<ActionResult<bool>> DeletePersonalBooking([FromRoute] Guid id) => HandleResult(await _personalBookingService.DeletePersonalBooking(id));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonalBookingInfoResponse>> GetPersonalBooking([FromRoute] Guid id) => HandleResult(await _personalBookingService.GetPersonalBookingAsync(id));

    [HttpGet("client-personal-bookings/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<PersonalBookingResponse>>> GetAllClientPersonalBookings([FromRoute] Guid clientId) => HandleListedResult(await _personalBookingService.GetAllClientPersonalBookings(clientId));

    [HttpDelete("{personalBookingId:guid}")]
    public async Task<ActionResult> DeleteClientPersonalBooking([FromRoute] Guid personalBookingId) => HandleResult(await _personalBookingService.DeletePersonalBookingAsync(personalBookingId));

    [HttpGet]
    public async Task<ActionResult<PersonalBookingInfoResponse>> GetWebAllPersonalBookingsByClientId() => HandleResult(await _personalBookingService.GetAllPersonalBookingsByClientIdAsync());

    [HttpGet("personal-booking-for-edit/{personalBookingId:guid}")]
    public async Task<ActionResult<PersonalBookingForEditResponse>> GetPersonalBookingForEdit([FromRoute] Guid personalBookingId) => HandleResult(await _personalBookingService.GetPersonalBookingForEditAsync(personalBookingId));

    [HttpPost]
    public async Task<ActionResult<PersonalBookingInfoResponse>> CreatePersonalBooking([FromBody] PersonalBookingAddRequest entity) => HandleResult(await _personalBookingService.CreatePersonalBookingAsync(entity));

    [HttpPut("pay-client/{id:guid}")]
    public async Task<ActionResult<PersonalBookingInfoResponse>> SetStatusToPaid(Guid id) => HandleResult(await _personalBookingService.SetStatusToPaidAsync(id));
    
}
