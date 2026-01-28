using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IScheduledClassService
{
    Task<Result<Unit>> CancelScheduleClassAsync(Guid scheduleClassId);
    Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(Guid gymClassId, string? searchText);
    Task<Result<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId(Guid gymClassId, Guid membershipId, Guid clientId);
    Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id);
}