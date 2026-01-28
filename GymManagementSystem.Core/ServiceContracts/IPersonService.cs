using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IPersonService
{
    Task<Result<Unit>> AddPersonToStaffAsync(PersonAddRequest request);
    Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync(string? searchText, bool? isTrainer, EmployeeRole? employeeRole, TrainerTypeEnum? trainerTypeEnum, bool? isActive);
    Task<Result<PersonDetailsResponse>> GetPersonDetailsAsync(Guid personId);
    Task<Result<PersonForEditResponse>> GetPersonForEditAsync(Guid personId);
    Task<Result<Unit>> UpdatePersonAsync(PersonUpdateRequest request);
}
