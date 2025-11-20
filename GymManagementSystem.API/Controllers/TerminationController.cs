using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;


public class TerminationController : BaseController
{
    private readonly IServiceAdder<TerminationResponse, TerminationAddRequest> _serviceAdder;
    private readonly IServiceReader<TerminationResponse> _serviceReader;
    private readonly ITerminationValidator _terminationValidator;

    public TerminationController(IServiceAdder<TerminationResponse, TerminationAddRequest> serviceAdder, IServiceReader<TerminationResponse> serviceReader, ITerminationValidator terminationValidator)
    {
        _terminationValidator = terminationValidator;
        _serviceAdder = serviceAdder;
        _serviceReader = serviceReader;
    }

    [HttpPost]
    public async Task<ActionResult<TerminationResponse>> PostTermination([FromBody] TerminationAddRequest request, CancellationToken cancellationToken)
    => HandleResult(await _serviceAdder.CreateAsync(request, cancellationToken));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Termination>>> GetTerminations(CancellationToken cancellationToken)
    => HandleListedResult(await _serviceReader.GetAllAsync(cancellationToken));

    [HttpGet("{clientId:guid}/can-create-termination")]
    public async Task<ActionResult<bool>> CanCreateTermination([FromRoute]
    Guid clientId) => HandleResult(await _terminationValidator.CanCreateTerminationAsync(clientId));

}
