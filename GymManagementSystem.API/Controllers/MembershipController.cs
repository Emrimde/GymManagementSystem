using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.API.Controllers;

public class MembershipController : BaseCrudController<MembershipResponse, MembershipAddRequest, MembershipUpdateRequest, Membership>
{
    public MembershipController(IService<MembershipResponse, MembershipAddRequest, MembershipUpdateRequest, Membership> service) : base(service)
    {
    }
}
