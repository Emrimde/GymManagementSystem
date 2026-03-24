using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace GymManagementSystem.Core.Services;

public class ClassBookingService : IClassBookingService
{
    private readonly IClassBookingRepository _classBookingRepo;
    private readonly IClientMembershipRepository _clientMembershipRepo;
    private readonly IScheduledClassRepository _scheduledClassRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymClassRepository _gymClassRepo;
    private readonly IHttpContextAccessor _contextAccessor;

    public ClassBookingService(IClassBookingRepository classBookingRepo, IClientMembershipRepository clientMembershipRepo, IScheduledClassRepository scheduledClassRepository, IGymClassRepository gymClassRepo, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
    {
        _classBookingRepo = classBookingRepo;
        _clientMembershipRepo = clientMembershipRepo;
        _scheduledClassRepository = scheduledClassRepository;
        _gymClassRepo = gymClassRepo;
        _contextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request)
    {
        if (!request.IsRequestFromWeb)
        {
            ClientMembership? clientMembership = await _clientMembershipRepo.GetActiveClientMembershipByClientId(request.ClientId);
            if (clientMembership == null)
            {
                return Result<ClassBookingInfoResponse>.Failure("Unable to book class for client because he doesn't have active membership", StatusCodeEnum.BadRequest);
            }

        }

        ClassBooking classBooking = request.ToClassBooking();
        if (request.IsRequestFromWeb)
        {
            string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
            if (!Guid.TryParse(claim, out var parsedClientId))
            {
                return Result<IEnumerable<ClassBookingResponse>>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
            }
            classBooking.ClientId = parsedClientId;
        }
        else
        {
            classBooking.ClientId = request.ClientId;
        }


        ScheduledClass? scheduledClass = await _scheduledClassRepository.GetByIdAsync(request.ScheduledClassId);
        if (scheduledClass == null)
        {
            return Result<ClassBookingInfoResponse>.Failure("Scheduled class not found", StatusCodeEnum.NotFound);
        }

        if (scheduledClass.ClassBookings.Any(item => item.Client.Id == classBooking.ClientId)) 
        {
            return Result<ClassBookingInfoResponse>.Failure("Client booked for this classes", StatusCodeEnum.BadRequest);
        }

        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(scheduledClass.GymClassId);


        int classBookingsCount = scheduledClass.ClassBookings.Count();
        if (classBookingsCount == gymClass!.MaxPeople)
        {
            return Result<ClassBookingInfoResponse>.Failure("Unable to book client because max people reached", StatusCodeEnum.BadRequest);
        }


        _classBookingRepo.CreateAsync(classBooking);
        await _unitOfWork.SaveChangesAsync();
        return Result<ClassBookingInfoResponse>.Success(classBooking.ToClassBookingInfo());
    }

    public async Task<Result<Unit>> DeleteClassBookingAsync(Guid classBookingId)
    {
        bool exists = await _classBookingRepo.DeleteClassBookingAsync(classBookingId);
        if (!exists)
        {
            return Result<Unit>.Failure("Not Found", StatusCodeEnum.NotFound);
        }

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }

    public async Task<Result<IEnumerable<ClassBookingResponse>>> GetAllByClientIdAsync(Guid? clientId)
    {
        IEnumerable<ClassBookingReadModel> classBookings = new List<ClassBookingReadModel> { };
        if (clientId == null)
        {
            string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
            if (!Guid.TryParse(claim, out var parsedClientId))
            {
                return Result<IEnumerable<ClassBookingResponse>>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
            }

            classBookings = await _classBookingRepo.GetAllClassBookingsByClientId(parsedClientId);
        }
        else
        {
            classBookings = await _classBookingRepo.GetAllClassBookingsByClientId(clientId.Value);
        }
        return Result<IEnumerable<ClassBookingResponse>>.Success(classBookings.Select(item => item.ToClassBookingResponse()));
    }
}
