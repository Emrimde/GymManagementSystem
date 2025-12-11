using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class MembershipController : BaseController
{
    private readonly IMembershipService _membershipService;
    public MembershipController(IMembershipService membershipService)
    {

        _membershipService = membershipService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipResponse>>> GetAll()
        => HandleListedResult(await _membershipService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MembershipResponse>> GetMembershipById(Guid id)
        => HandleResult(await _membershipService.GetByIdAsync(id));

    [HttpPost]
    public async Task<ActionResult<MembershipResponse>> CreateMembership([FromBody] MembershipAddRequest entity)
        => HandleResult(await _membershipService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MembershipResponse>> UpdateMembership(Guid id, MembershipUpdateRequest entity)
        => HandleResult(await _membershipService.UpdateAsync(id, entity));
}
