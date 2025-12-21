using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
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
    private readonly IUnitOfWork _unitOfWork;
    public PersonService(IPersonRepository personRepo,IUnitOfWork unitOfWork)
    {
        _personRepo = personRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PersonInfoResponse>> AddPersonToStaffAsync(PersonAddRequest request)
    {
        Person personAdd = request.ToPerson();
        Guid personId = _personRepo.AddPersonToStaff(personAdd);
        PersonInfoResponse personInfoResponse = new PersonInfoResponse() { PersonId  = personId };
        await _unitOfWork.SaveChangesAsync();
        return Result<PersonInfoResponse>.Success(personInfoResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync()
    {
        IEnumerable<PersonReadModel> readModels = await _personRepo.GetAllStaffAsync();
        return Result<IEnumerable<PersonResponse>>.Success(readModels.Select(item => item.ToPersonResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<PersonDetailsResponse>> GetPersonDetailsAsync(Guid personId)
    {
       Person? person = await _personRepo.GetPersonByIdAsync(personId);
        if (person == null)
        {
            return Result<PersonDetailsResponse>.Failure("Person not found", StatusCodeEnum.BadRequest);
        }
       return Result<PersonDetailsResponse>.Success(person.ToPersonDetailsResponse(), StatusCodeEnum.Ok);
       
    }
}
