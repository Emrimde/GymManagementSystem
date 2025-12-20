using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.Person.ReadModel;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepo;
    public PersonService(IPersonRepository personRepo)
    {
        _personRepo = personRepo;
    }

    public async Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync()
    {
        IEnumerable<PersonReadModel> readModels = await _personRepo.GetAllStaffAsync();
        return Result<IEnumerable<PersonResponse>>.Success(readModels.Select(item => item.ToPersonResponse()), StatusCodeEnum.Ok);
    }
}
