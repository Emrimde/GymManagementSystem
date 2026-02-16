using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Client.QueryDto;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class ClientController : BaseController
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet]
    public async Task<ActionResult<PageResult<ClientResponse>>> GetAll([FromQuery] GetClientQueryDto query)
        => HandlePageResult(await _clientService.GetAllAsync(query));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDetailsResponse>> GetById(Guid id)
        => HandleResult(await _clientService.GetByIdAsync(id));

    [HttpGet("get-for-edit/{id:guid}")]
    public async Task<ActionResult<ClientEditResponse>> GetByIdForEdit(Guid id)
        => HandleResult(await _clientService.GetByIdForEditAsync(id));

    [HttpGet("name/{id:guid}")]
    public async Task<ActionResult<ClientInfoResponse>> GetClientFullNameById(Guid id)
        => HandleResult(await _clientService.GetClientFullNameByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<ClientInfoResponse>> Create([FromBody] ClientAddRequest entity)
        => HandleResult(await _clientService.CreateAsync(entity));

    [HttpPost("validate")]
    public ActionResult<ClientInfoResponse> ValidateClientAge([FromBody] ClientAgeValidationRequest entity)
        => HandleResult(_clientService.ValidateClientAgeAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientInfoResponse>> Update(Guid id, ClientUpdateRequest entity)
        => HandleResult(await _clientService.UpdateAsync(id, entity));

    [HttpGet("lookup")]
    public async Task<ActionResult<IEnumerable<ClientInfoResponse>>> LookUpClients([FromQuery] string query, [FromQuery] Guid? scheduledClassId = null) => HandleResult(await _clientService.LookUpClientsAsync(query, scheduledClassId));
}
