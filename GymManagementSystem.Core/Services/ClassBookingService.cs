using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClassBookingService : IClassBookingService
{
    private readonly IClassBookingRepository _classBookingRepo;
    private readonly IClientMembershipRepository _clientMembershipRepo;
    private readonly IScheduledClassRepository _scheduledClassRepository;
    private readonly IGymClassRepository _gymClassRepo;
    
    public ClassBookingService(IClassBookingRepository classBookingRepo, IClientMembershipRepository clientMembershipRepo, IScheduledClassRepository scheduledClassRepository, IGymClassRepository gymClassRepo)
    {
        _classBookingRepo = classBookingRepo;
        _clientMembershipRepo = clientMembershipRepo;
        _scheduledClassRepository = scheduledClassRepository;
        _gymClassRepo = gymClassRepo;
    }
    public async Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request)
    {
        ClientMembership? clientMembership = await _clientMembershipRepo.GetActiveClientMembershipByClientId(request.ClientId);
        if(clientMembership == null) 
        {
            return Result<ClassBookingInfoResponse>.Failure("Unable to book class for client because he doesn't have active membership", StatusCodeEnum.BadRequest);
        }

        ScheduledClass? scheduledClass = await _scheduledClassRepository.GetByIdAsync(request.ScheduledClassId);
        if (scheduledClass == null) 
        {
            return Result<ClassBookingInfoResponse>.Failure("Scheduled class not found", StatusCodeEnum.NotFound); 
        }

        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(scheduledClass.GymClassId);

        if(clientMembership.MembershipStatus == MembershipStatusEnum.Upcoming) 
        {
            return Result<ClassBookingInfoResponse>.Failure("Client's memebrship status is upcoming", StatusCodeEnum.BadRequest);
        }

        int classBookingsCount = scheduledClass.ClassBookings.Count();
        if(classBookingsCount == gymClass!.MaxPeople)
        {
            return Result<ClassBookingInfoResponse>.Failure("Unable to book client because max people reached", StatusCodeEnum.BadRequest);
        }

            ClassBooking addedClassBooking = await _classBookingRepo.CreateAsync(request.ToClassBooking());
        return Result<ClassBookingInfoResponse>.Success(addedClassBooking.ToClassBookingInfo());
    }

    public async Task<Result<IEnumerable<ClassBookingResponse>>> GetAllByClientIdAsync(Guid clientId)
    {
        IEnumerable<ClassBookingReadModel> classBookings = await _classBookingRepo.GetAllClassBookingsByClientId(clientId);
        return Result<IEnumerable<ClassBookingResponse>>.Success(classBookings.Select(item => item.ToClassBookingResponse()));
    }

    public Task<Result<ClassBookingDetailsResponse>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
