using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class ClientController : BaseController
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponse>>> GetAll(CancellationToken cancellationToken)
        => HandleListedResult(await _clientService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientResponse>> GetById(Guid id,[FromQuery] bool isActiveOnly,  CancellationToken cancellationToken)
        => HandleResult(await _clientService.GetByIdAsync(id,isActiveOnly, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ClientInfoResponse>> Create([FromBody] ClientAddRequest entity)
        => HandleResult(await _clientService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientInfoResponse>> Update(Guid id, ClientUpdateRequest entity)
        => HandleResult(await _clientService.UpdateAsync(id, entity));

    [HttpGet("lookup")]
    public async Task<ActionResult<IEnumerable<ClientInfoResponse>>> LookUpClients([FromQuery] string query, [FromQuery] Guid scheduledClassId) => HandleResult(await _clientService.LookUpClientsAsync(query, scheduledClassId));
}
