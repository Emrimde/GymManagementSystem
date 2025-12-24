using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
namespace GymManagementSystem.Core.Services;

public class TerminationService : ITerminationService
{
    private readonly IRepository<TerminationResponse,Termination> _terminationRepo;
    private readonly IContractRepository _contractRepo;
    private readonly IClientMembershipRepository _clientMembershipRepo;
    private readonly IUnitOfWork _unitOfWork;
    public TerminationService(IRepository<TerminationResponse,Termination> terminationRepository, IContractRepository contractRepository,IUnitOfWork unitOfWork, IClientMembershipRepository clientMembershipRepo) 
    {
        _terminationRepo = terminationRepository;
        _contractRepo = contractRepository;
        _unitOfWork = unitOfWork;
        _clientMembershipRepo = clientMembershipRepo;
    }

    public async Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity)
    {
        ClientMembership? activeMembership = await _clientMembershipRepo.GetActiveClientMembershipByClientId(entity.ClientId);
        if (activeMembership == null)
        {
            return Result<TerminationResponse>.Failure("Error: Termination cannot be created because client doesn't have active membership");
        }
        
        activeMembership.IsActive = false;
        Termination termination = entity.ToTermination(activeMembership.Id);
        Termination createdTermination = await _terminationRepo.CreateAsync(termination);
        //await _unitOfWork.SaveChangesAsync();  
        return Result<TerminationResponse>.Success(createdTermination.ToTerminationResponse(), StatusCodeEnum.Ok);
    }
}
