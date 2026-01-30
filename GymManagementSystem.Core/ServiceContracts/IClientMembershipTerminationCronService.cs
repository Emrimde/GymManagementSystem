namespace GymManagementSystem.Core.ServiceContracts;

public interface IClientMembershipTerminationCronService
{
    Task DeactivateExpiredClientMemberships();
}
