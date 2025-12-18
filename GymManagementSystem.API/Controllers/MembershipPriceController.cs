using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class MembershipPriceController : BaseController
{
    private readonly IMembershipPriceService _membershipPriceService;
    public MembershipPriceController(IMembershipPriceService membershipPriceService)
    {
        _membershipPriceService = membershipPriceService;
    }

    [HttpGet("{membershipId:guid}")]
    public async Task<ActionResult<IEnumerable<MembershipPriceResponse>>> GetMembershipPricesByMembershipId([FromRoute] Guid membershipId)
    => HandleListedResult(await _membershipPriceService.GetMembershipPricesByMembershipIdAsync(membershipId));


    [HttpPost]
    public async Task<ActionResult> CreateMembershipPrice([FromBody] MembershipPriceAddRequest membershipPriceAddRequest) => HandleResult(await _membershipPriceService.CreateMembershipPriceAsync(membershipPriceAddRequest));
    
}
