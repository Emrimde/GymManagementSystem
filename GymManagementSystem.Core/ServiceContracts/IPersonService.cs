using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IPersonService
{
    Task<Result<Unit>> AddPersonToStaffAsync(PersonAddRequest request);
    Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync(string? searchText);
    Task<Result<PersonDetailsResponse>> GetPersonDetailsAsync(Guid personId);
    Task<Result<PersonForEditResponse>> GetPersonForEditAsync(Guid personId);
    Task<Result<Unit>> UpdatePersonAsync(PersonUpdateRequest request);
}
