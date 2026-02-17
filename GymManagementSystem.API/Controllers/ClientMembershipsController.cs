using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class ClientMembershipsController : BaseController
{
    private readonly IClientMembershipService _clientMembershipService;
    public ClientMembershipsController(IClientMembershipService clientMembershipService)
    {
        _clientMembershipService = clientMembershipService;
    }

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("membership-history/{clientId:guid}")]
    public async Task<ActionResult<PageResult<ClientMembershipResponse>>> GetAllMembershipsClientHistory([FromRoute] Guid clientId)
            => HandleListedResult(await _clientMembershipService.GetAllMembershipsClientHistoryAsync(clientId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("contract-preview/{clientId:guid}/{membershipId:guid}")]
    public async Task<ActionResult<ClientMembershipContractPreviewResponse>> GetContractPreviewDetails([FromRoute] Guid clientId, [FromRoute] Guid membershipId)
            => HandleResult(await _clientMembershipService.GetContractPreviewDetailsAsync(clientId,membershipId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("{clientMembershipId:guid}")]
    public async Task<ActionResult<ClientMembershipDetailsResponse>> GetById(Guid clientMembershipId)
        => HandleResult(await _clientMembershipService.GetByIdAsync(clientMembershipId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPost]
    public async Task<ActionResult<ClientMembershipInfoResponse>> Create([FromBody] ClientMembershipAddRequest entity)
        => HandleResult(await _clientMembershipService.CreateAsync(entity));
}
