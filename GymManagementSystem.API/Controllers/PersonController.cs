using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class PersonController : BaseController
{
    private readonly IPersonService _personService;
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAllStaff() => HandleListedResult(await _personService.GetAllStaffAsync());
    [HttpGet("{personId:guid}")]
    public async Task<ActionResult<IEnumerable<PersonDetailsResponse>>> GetPersonDetails([FromRoute] Guid personId) => HandleResult(await _personService.GetPersonDetailsAsync(personId));

    [HttpGet("get-person-for-edit/{personId:guid}")]
    public async Task<ActionResult<PersonForEditResponse>> GetPersonForEdit([FromRoute] Guid personId) => HandleResult(await _personService.GetPersonForEditAsync(personId));

    [HttpPost]
    public async Task<ActionResult<PersonInfoResponse>> AddPersonToStaff([FromBody] PersonAddRequest request) => HandleResult(await _personService.AddPersonToStaffAsync(request));

    [HttpPut]
    public async Task<ActionResult> UpdatePerson([FromBody] PersonUpdateRequest request) => HandleResult(await _personService.UpdatePersonAsync(request));
}
