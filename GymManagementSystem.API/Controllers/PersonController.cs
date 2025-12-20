using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
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

    [HttpPost]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> AddPersonToStaff() => HandleListedResult(await _personService.GetAllStaffAsync());
}
