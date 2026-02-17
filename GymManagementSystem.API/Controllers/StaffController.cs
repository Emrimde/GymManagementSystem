using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class StaffController : BaseController
{
    private readonly IPersonService _personService;
    public StaffController(IPersonService personService)
    {
        _personService = personService;
    }

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAllStaff([FromQuery] string? searchText, [FromQuery] bool? isTrainer, [FromQuery] EmployeeRole? employeeRole , [FromQuery] TrainerTypeEnum? trainerType, [FromQuery] bool? isActive) => HandleListedResult(await _personService.GetAllStaffAsync(searchText, isTrainer, employeeRole, trainerType, isActive));

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("{personId:guid}")]
    public async Task<ActionResult<IEnumerable<PersonDetailsResponse>>> GetPersonDetails([FromRoute] Guid personId) => HandleResult(await _personService.GetPersonDetailsAsync(personId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("get-person-for-edit/{personId:guid}")]
    public async Task<ActionResult<PersonForEditResponse>> GetPersonForEdit([FromRoute] Guid personId) => HandleResult(await _personService.GetPersonForEditAsync(personId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost]
    public async Task<ActionResult<Unit>> AddPersonToStaff([FromBody] PersonAddRequest request) => HandleResult(await _personService.AddPersonToStaffAsync(request));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut]
    public async Task<ActionResult> UpdatePerson([FromBody] PersonUpdateRequest request) => HandleResult(await _personService.UpdatePersonAsync(request));
}
