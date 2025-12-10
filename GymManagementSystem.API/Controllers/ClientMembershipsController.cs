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

    [HttpGet]
    public async Task<ActionResult<PageResult<ClientMembershipResponse>>> GetAll([FromQuery] string? searchText, [FromQuery] int pageSize = 50, [FromQuery] int page = 1)
            => HandlePageResult(await _clientMembershipService.GetAllAsync(searchText,pageSize,page));

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
