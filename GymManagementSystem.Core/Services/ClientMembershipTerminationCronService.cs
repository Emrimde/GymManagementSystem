using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.ServiceContracts;
namespace GymManagementSystem.Core.Services;
public class ClientMembershipTerminationCronService : IClientMembershipTerminationCronService
{
    private readonly IClientMembershipRepository _clientMembershipRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ClientMembershipTerminationCronService(IClientMembershipRepository clientMembershipRepository, IUnitOfWork unitOfWork)
    {
        _clientMembershipRepository = clientMembershipRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task DeactivateExpiredClientMemberships()
    {
        IEnumerable<ClientMembership> memberships = await _clientMembershipRepository
            .GetMembershipsToDeactivate();

        foreach (var item in memberships)
        {
            item.IsActive = false;

            if (item.Termination != null)
            {
                item.Termination.IsActive = false;
            }

            if (item.Client != null)
            {
                item.Client.IsActive = false;
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
