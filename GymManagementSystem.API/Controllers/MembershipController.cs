using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Membership;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Owner,Manager,Receptionist,GroupInstructor,Trainer")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipResponse>>> GetAll()
        => HandleListedResult(await _membershipService.GetAllAsync());

    [Authorize(Roles = "Owner,Manager,Receptionist,GroupInstructor,Trainer")]
    [HttpGet("membership-name/{membershipId:guid}")]
    public async Task<ActionResult<MembershipInfoResponse>> GetMembershipName([FromRoute] Guid membershipId)
        => HandleResult(await _membershipService.GetMembershipNameAsync(membershipId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("{membershipId:guid}")]
    public async Task<ActionResult<MembershipResponse>> GetMembershipById(Guid membershipId)
        => HandleResult(await _membershipService.GetByIdAsync(membershipId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("create-membership-feature")]
    public async Task<ActionResult> CreateMembershipFeature([FromBody] MembershipFeatureAddRequest entity)
        => HandleResult(await _membershipFeatureService.CreateMembershipFeatureAsync(entity));

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("get-membership-features/{membershipId:guid}")]
    public async Task<ActionResult<IEnumerable<MembershipFeatureResponse>>> GetMembershipFeaturesByMembershipId([FromRoute] Guid membershipId)
        => HandleListedResult(await _membershipFeatureService.GetMembershipFeaturesByMembershipIdAsync(membershipId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Unit>> UpdateMembership(Guid id, MembershipUpdateRequest entity)
        => HandleResult(await _membershipService.UpdateAsync(id, entity));

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("get-membership-feature-for-edit/{membershipFeatureId:guid}")]
    public async Task<ActionResult<MembershipFeatureForEditResponse>> GetMembershipFeatureForEdit([FromRoute] Guid membershipFeatureId)
        => HandleResult(await _membershipFeatureService.GetMembershipFeatureForEditAsync(membershipFeatureId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut("update-membership-feature")]
    public async Task<ActionResult<Unit>> UpdateMembershipFeature([FromBody] MembershipFeatureUpdateRequest entity)
        => HandleResult(await _membershipFeatureService.UpdateMembershipFeatureAsync(entity));

    [Authorize(Roles = "Owner,Manager")]
    [HttpDelete("delete-membership-feature/{membershipFeatureId:guid}")]
    public async Task<ActionResult<Unit>> HardDeleteMembershipFeature([FromRoute] Guid membershipFeatureId)
        => HandleResult(await _membershipFeatureService.HardDeleteMembershipFeatureAsync(membershipFeatureId));

    [HttpGet("get-all-memberships")]
    public async Task<ActionResult<IEnumerable<MembershipWebDetailsResponse>>> GetAllMembershipsWithFeatures()
       => HandleListedResult(await _membershipService.GetAllMembershipsWithFeaturesAsync());
}
