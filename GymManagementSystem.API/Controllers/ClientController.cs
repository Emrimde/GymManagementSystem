using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymManagementSystem.API.Controllers;

public class ClientController : BaseController
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<ClientResponse>>> GetAll([FromQuery] string? searchText, [FromQuery] int page)
        => HandlePageResult(await _clientService.GetAllAsync(searchText, page));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientResponse>> GetById(Guid id,[FromQuery] bool isActiveOnly)
        => HandleResult(await _clientService.GetByIdAsync(id,isActiveOnly));

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
