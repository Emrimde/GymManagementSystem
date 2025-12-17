using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using System.Runtime.CompilerServices;

namespace GymManagementSystem.Core.Services;

public class VisitService : IVisitService
{
    private readonly IVisitRepository _visitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepo;
    private readonly IClientMembershipRepository _clientMembershipRepo;
    public VisitService(IVisitRepository visitRepository, IUnitOfWork unitOfWork, IClientRepository clientRepo, IClientMembershipRepository clientMembershipRepo)
    {
        _visitRepository = visitRepository;
        _unitOfWork = unitOfWork;
        _clientRepo = clientRepo;
        _clientMembershipRepo = clientMembershipRepo;
    }

    public async Task<Result<Unit>> RegisterVisitAsync(Guid clientId)
    {
        Client? client = await _clientRepo.GetByIdAsync(clientId);
        if (client == null)
        {
            return Result<Unit>.Failure("Error with adding visit", StatusCodeEnum.NotFound);
        }

        Visit visit = new Visit
        {
            ClientId = clientId,
            VisitDate = DateTime.UtcNow
        };

        ClientMembership? clientMembership = await _clientMembershipRepo.GetActiveClientMembershipByClientId(clientId);

        if (clientMembership == null)
        {
            visit.VisitSource = VisitSourceEnum.FromSingleVisit;
        }
        else
        {
            visit.ClientMembershipId = clientMembership.Id;
            visit.VisitSource = VisitSourceEnum.FromMembership;
        }

        _visitRepository.AddVisit(visit);
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }
}
