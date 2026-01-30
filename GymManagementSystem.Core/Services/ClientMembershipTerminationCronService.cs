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
        IEnumerable<ClientMembership> clientMemberships = await _clientMembershipRepository.GetAllClientMembershipsWithActiveTermination();
        foreach (ClientMembership clientMembership in clientMemberships)
        {
            if (clientMembership.EndDate!.Value.Date <= DateTime.UtcNow.Date)
            {
                clientMembership.IsActive = false;
                clientMembership.Termination!.IsActive = false;
                clientMembership.Client!.IsActive = false;
            }
        }
        await _unitOfWork.SaveChangesAsync();
    }
}
