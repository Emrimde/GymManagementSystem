using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IPersonService
{
    Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync();
}
