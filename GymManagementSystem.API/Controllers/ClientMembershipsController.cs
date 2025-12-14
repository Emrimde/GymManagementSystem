using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Result;
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

    [HttpGet("membership-history/{clientId:guid}")]
    public async Task<ActionResult<PageResult<ClientMembershipResponse>>> GetAllMembershipsClientHistory([FromRoute] Guid clientId)
            => HandleListedResult(await _clientMembershipService.GetAllMembershipsClientHistoryAsync(clientId));    
    
    [HttpGet("contract-preview/{clientId:guid}/{membershipId:guid}")]
    public async Task<ActionResult<ClientMembershipContractPreviewResponse>> GetContractPreviewDetails([FromRoute] Guid clientId, [FromRoute] Guid membershipId)
            => HandleResult(await _clientMembershipService.GetContractPreviewDetailsAsync(clientId,membershipId));

    [HttpGet("{clientMembershipId:guid}")]
    public async Task<ActionResult<ClientMembershipDetailsResponse>> GetById(Guid clientMembershipId)
        => HandleResult(await _clientMembershipService.GetByIdAsync(clientMembershipId));

    [HttpPost]
    public async Task<ActionResult<ClientMembershipInfoResponse>> Create([FromBody] ClientMembershipAddRequest entity)
        => HandleResult(await _clientMembershipService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientMembershipInfoResponse>> Update(Guid id, ClientMembershipUpdateRequest entity)
        => HandleResult(await _clientMembershipService.UpdateAsync(id, entity));
}
