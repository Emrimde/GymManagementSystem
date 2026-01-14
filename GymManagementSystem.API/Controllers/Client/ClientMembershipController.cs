using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.ClientMembership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers.Client;

[Route("api/client/client-membership")]
[ApiController]
public class ClientMembershipController : BaseController
{
    private readonly IClientMembershipService _clientMembershipService;
    public ClientMembershipController(IClientMembershipService clientMembershipService)
    {
        _clientMembershipService = clientMembershipService;
    }

    [Authorize(Roles = "Client")]
    [HttpGet("get-client-membership-info")]
    public async Task<ActionResult<ClientMembershipWebResponse?>> GetClientMembershipWebInformation() => HandleResult(await _clientMembershipService.GetClientMembershipInfoAsync());

    [Authorize(Roles = "Client")]
    [HttpGet("get-client-membership-preview/{membershipId:guid}")]
    public async Task<ActionResult<ClientMembershipWebPreviewResponse?>> GetClientMembershipWebPreview([FromRoute] Guid membershipId) => HandleResult(await _clientMembershipService.GetClientMembershipPreviewAsync(membershipId));
}
