using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Membership;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class MembershipController : BaseController
{
    private readonly IMembershipService _membershipService;
    private readonly IMembershipFeatureService _membershipFeatureService;
    public MembershipController(IMembershipService membershipService, IMembershipFeatureService membershipFeatureService)
    {

        _membershipService = membershipService;
        _membershipFeatureService = membershipFeatureService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipResponse>>> GetAll()
        => HandleListedResult(await _membershipService.GetAllAsync());

    [HttpGet("membership-name/{membershipId:guid}")]
    public async Task<ActionResult<MembershipInfoResponse>> GetMembershipName([FromRoute] Guid membershipId)
        => HandleResult(await _membershipService.GetMembershipNameAsync(membershipId));

    [HttpGet("{membershipId:guid}")]
    public async Task<ActionResult<MembershipResponse>> GetMembershipById(Guid membershipId)
        => HandleResult(await _membershipService.GetByIdAsync(membershipId));

    [HttpPost]
    public async Task<ActionResult<MembershipResponse>> CreateMembership([FromBody] MembershipAddRequest entity)
        => HandleResult(await _membershipService.CreateAsync(entity));

    [HttpPost("create-membership-feature")]
    public async Task<ActionResult> CreateMembershipFeature([FromBody] MembershipFeatureAddRequest entity)
        => HandleResult(await _membershipFeatureService.CreateMembershipFeatureAsync(entity));

    [HttpGet("get-membership-features/{membershipId:guid}")]
    public async Task<ActionResult<IEnumerable<MembershipFeatureResponse>>> GetMembershipFeaturesByMembershipId([FromRoute] Guid membershipId)
        => HandleListedResult(await _membershipFeatureService.GetMembershipFeaturesByMembershipIdAsync(membershipId));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MembershipResponse>> UpdateMembership(Guid id, MembershipUpdateRequest entity)
        => HandleResult(await _membershipService.UpdateAsync(id, entity));

    [HttpGet("get-all-memberships")]
    public async Task<ActionResult<IEnumerable<MembershipWebDetailsResponse>>> GetAllMembershipsWithFeatures()
       => HandleListedResult(await _membershipService.GetAllMembershipsWithFeaturesAsync());


}
