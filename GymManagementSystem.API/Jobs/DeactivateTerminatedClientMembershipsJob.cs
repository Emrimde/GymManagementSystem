using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.API.Jobs
{
    public class DeactivateTerminatedClientMembershipsJob
    {
        private readonly IClientMembershipTerminationCronService _clientMembershipCronService;
        public DeactivateTerminatedClientMembershipsJob(IClientMembershipTerminationCronService clientMembershipCronService)
        {
            _clientMembershipCronService = clientMembershipCronService;
        }

        public Task Run()
            => _clientMembershipCronService.DeactivateExpiredClientMemberships();
    }
}
