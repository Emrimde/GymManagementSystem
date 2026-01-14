using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementSystem.API.Controllers.Client;

[Route("api/client/client")]
public class ClientController : BaseController
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Authorize(Roles = "Client")]
    [HttpGet("get-client-details")]
    public async Task<ActionResult<ClientDetailsWebResponse>> GetClientWebProfileInfo()
       => HandleResult(await _clientService.GetClientProfileInfoAsync());


    [Authorize(Roles = "Client")]
    [HttpGet("get-client-context")]
    public async Task<ActionResult<ClientMembershipInformationResponse>> GetClientWebContext()
        => HandleResult(await _clientService.GetClientContextAsync());


    [Authorize(Roles = "Client")]
    [HttpPut("update-client")]
    public async Task<ActionResult> UpdateWebClientInfo([FromBody] ClientWebUpdateRequest updateRequest)
        => HandleResult(await _clientService.UpdateWebClientInfoAsync(updateRequest));

}
