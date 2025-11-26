using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class ClientMembershipsController : BaseController
{
    private readonly IClientMembershipService _clientMembershipService;
    public ClientMembershipsController(IClientMembershipService clientMembershipService)
    {
        _clientMembershipService = clientMembershipService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientMembershipResponse>>> GetAll(CancellationToken cancellationToken)
            => HandleListedResult(await _clientMembershipService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientMembershipResponse>> GetById(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _clientMembershipService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ClientMembershipInfoResponse>> Create([FromBody] ClientMembershipAddRequest entity)
        => HandleResult(await _clientMembershipService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientMembershipInfoResponse>> Update(Guid id, ClientMembershipUpdateRequest entity)
        => HandleResult(await _clientMembershipService.UpdateAsync(id, entity));
}
