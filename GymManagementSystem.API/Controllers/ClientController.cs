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

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer,GroupInstructor")]
    [HttpGet]
    public async Task<ActionResult<PageResult<ClientResponse>>> GetAll([FromQuery] GetClientQueryDto query)
        => HandlePageResult(await _clientService.GetAllAsync(query));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDetailsResponse>> GetById(Guid id)
        => HandleResult(await _clientService.GetByIdAsync(id));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("get-for-edit/{id:guid}")]
    public async Task<ActionResult<ClientEditResponse>> GetByIdForEdit(Guid id)
        => HandleResult(await _clientService.GetByIdForEditAsync(id));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("name/{id:guid}")]
    public async Task<ActionResult<ClientInfoResponse>> GetClientFullNameById(Guid id)
        => HandleResult(await _clientService.GetClientFullNameByIdAsync(id));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPost]
    public async Task<ActionResult<ClientInfoResponse>> Create([FromBody] ClientAddRequest entity)
        => HandleResult(await _clientService.CreateAsync(entity));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPost("validate")]
    public ActionResult<ClientInfoResponse> ValidateClientAge([FromBody] ClientAgeValidationRequest entity)
        => HandleResult(_clientService.ValidateClientAgeAsync(entity));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientInfoResponse>> Update(Guid id, ClientUpdateRequest entity)
        => HandleResult(await _clientService.UpdateAsync(id, entity));
}
