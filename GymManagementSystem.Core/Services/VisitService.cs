using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO;
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
    private readonly IMembershipRepository _membershipRepository;
    public VisitService(IVisitRepository visitRepository, IUnitOfWork unitOfWork, IClientRepository clientRepo, IClientMembershipRepository clientMembershipRepo, IMembershipRepository membershipRepo)
    {
        _visitRepository = visitRepository;
        _unitOfWork = unitOfWork;
        _clientRepo = clientRepo;
        _clientMembershipRepo = clientMembershipRepo;
        _membershipRepository = membershipRepo;
    }

    public async Task<Result<IEnumerable<VisitResponse>>> GetAllClientVisitsAsync(Guid clientId)
    {
        IEnumerable<VisitResponse> visits = await _visitRepository.GetAllClientVisits(clientId);
        return Result<IEnumerable<VisitResponse>>.Success(visits, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> RegisterVisitAsync(Guid clientId, string? guestName)
    {
        Client? client = await _clientRepo.GetByIdAsync(clientId);
        if (client == null)
        {
            return Result<Unit>.Failure("Error with adding visit", StatusCodeEnum.NotFound);
        }

        Visit visit = new Visit
        {
            ClientId = clientId,
            VisitDate = DateTime.UtcNow,
            IsWithGuest = false
        };

        ClientMembership? clientMembership = await _clientMembershipRepo.GetActiveClientMembershipByClientId(clientId);
        
        if (clientMembership == null)
        {
            visit.VisitSource = VisitSourceEnum.FromSingleVisit;
        }
        else
        {
            int freeFriendVisits = await _membershipRepository.GetFreeFriendArrivalsPerMonthAsync(clientMembership.MembershipId);
            int actualFriendVisitThisMonth = await _visitRepository.GetFriendVisitsCountForClientInMonthAsync(clientId);
            if(freeFriendVisits <= actualFriendVisitThisMonth)
            {
                return Result<Unit>.Failure("No more free friend visits available for this month", StatusCodeEnum.BadRequest);
            }

            visit.IsWithGuest = !string.IsNullOrEmpty(guestName);
            visit.GuestName = guestName;
            visit.ClientMembershipId = clientMembership.Id;
            visit.VisitSource = VisitSourceEnum.FromMembership;
        }

        _visitRepository.AddVisit(visit);
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> DeleteVisitAsync(Guid visitId)
    {
        Visit? visit = await _visitRepository.GetVisitById(visitId);
        if (visit == null)
        {
            return Result<Unit>.Failure("Visit not found", StatusCodeEnum.NotFound);
        }

        _visitRepository.DeleteVisit(visit);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }
}
