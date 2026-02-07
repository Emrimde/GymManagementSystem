using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymManagementSystem.Core.Services;

public class ScheduledClassService : IScheduledClassService
{
    private readonly IScheduledClassRepository _schedulecClassRepo;
    private readonly IMembershipRepository _memberhsipRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _http;

    public ScheduledClassService(IScheduledClassRepository schedulecClassRepo, IClassBookingRepository classBookingRepo, IMembershipRepository memberhsipRepository, IUnitOfWork unitOfWork, IClientRepository clientRepository, IHttpContextAccessor http)
    {
        _schedulecClassRepo = schedulecClassRepo;
        _memberhsipRepository = memberhsipRepository;
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
        _http = http;
    }

    public async Task<Result<Unit>> CancelScheduleClassAsync(Guid scheduleClassId)
    {
        ScheduledClass? scheduleClass = await _schedulecClassRepo.GetByIdAsync(scheduleClassId);
        if (scheduleClass == null)
        {
            return Result<Unit>.Failure("Scheduled class not found", StatusCodeEnum.NotFound);
        }
        scheduleClass.IsActive = false;
        foreach (ClassBooking classBooking in scheduleClass.ClassBookings)
        {
            classBooking.IsActive = false;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }


    public async Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(Guid gymClassId)
    {
        IEnumerable<ScheduledClassResponse> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClasses(gymClassId);
        return Result<IEnumerable<ScheduledClassResponse>>.Success(scheduledClasses, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId(Guid gymclassId, Guid? clientId)
    {
        ClientInfoResponse? client = null;

        if (clientId != null)
        {
            client = await _clientRepository.GetClientFullNameByIdAsync(clientId.Value);
        }
        else
        {
            string? clientIdFromToken = _http.HttpContext?.User.FindFirst("client_id")?.Value;

            if (clientIdFromToken != null && Guid.TryParse(clientIdFromToken, out Guid clientGuid))
            {
                client = await _clientRepository.GetClientFullNameByIdAsync(clientGuid);
            }
        }

        if (client == null)
        {
            return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        Membership? membership = await _memberhsipRepository.GetByIdAsync(client.MembershipId);
        if (membership == null)
        {
            return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        IEnumerable<ScheduledClass> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClassesByGymClassId(gymclassId, membership.ClassBookingDaysInAdvanceCount, true);


        IEnumerable<ScheduledClassComboBoxResponse> dto = scheduledClasses.OrderBy(item => item.Date).Select(item => item.ToScheduledClassComboBoxResponse());
        return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id)
    {
        ScheduledClass? scheduledClass = await _schedulecClassRepo.GetByIdAsync(id);
        if (scheduledClass == null)
        {
            return Result<ScheduledClassDetailsResponse>.Failure("Scheduled class not found", StatusCodeEnum.NotFound);
        }

        return Result<ScheduledClassDetailsResponse>.Success(scheduledClass.ToScheduledClassDetailsResponse(), StatusCodeEnum.Ok);
    }
}
