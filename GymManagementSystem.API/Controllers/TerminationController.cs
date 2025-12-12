using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class TerminationController : BaseController
{
    private readonly ITerminationService _terminationService;
    private readonly ITerminationValidator _terminationValidator;

    public TerminationController(ITerminationValidator terminationValidator, ITerminationService terminationService)
    {
        _terminationValidator = terminationValidator;
        _terminationService = terminationService;
    }

    [HttpPost]
    public async Task<ActionResult<TerminationResponse>> PostTermination([FromBody] TerminationAddRequest request, CancellationToken cancellationToken)
    => HandleResult(await _terminationService.CreateAsync(request));

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Termination>>> GetTerminations(CancellationToken cancellationToken)
    //=> HandleListedResult(await _terminationService.GetAllAsync());

    [HttpGet("{clientId:guid}/can-create-termination")]
    public async Task<ActionResult<bool>> CanCreateTermination([FromRoute]
    Guid clientId) => HandleResult(await _terminationValidator.CanCreateTerminationAsync(clientId));

}
