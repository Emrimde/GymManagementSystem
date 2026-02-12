using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.Person.ReadModel;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.Services;
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepo;
    private readonly IClientRepository _clientRepo;
    private readonly IUnitOfWork _unitOfWork;
    public PersonService(IPersonRepository personRepo,IUnitOfWork unitOfWork, IClientRepository clientRepo)
    {
        _personRepo = personRepo;
        _unitOfWork = unitOfWork;
        _clientRepo = clientRepo;
    }

    public async Task<Result<Unit>> AddPersonToStaffAsync(PersonAddRequest request)
    {
        Person personAdd = request.ToPerson();
        _personRepo.AddPersonToStaff(personAdd);
        bool exists = await _personRepo.ExistsByEmailOrPhoneAsync(request.Email, request.PhoneNumber);

        if (exists)
        {
            return Result<Unit>.Failure("Person with the same email or phone number already exists!", StatusCodeEnum.Conflict);
        }

        bool clientExists = await _clientRepo.ExistsByEmailOrPhoneAsync(request.Email, request.PhoneNumber);
        if (clientExists)
        {
            return Result<Unit>.Failure("Client with the same email or phone number already exists!", StatusCodeEnum.Conflict);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<PersonResponse>>> GetAllStaffAsync(string? searchText, bool? isTrainer, EmployeeRole? employeeRole, TrainerTypeEnum? trainerTypeEnum,  bool? isActive)
    {
        IEnumerable<PersonReadModel> readModels = await _personRepo.GetAllStaffAsync(searchText, isTrainer, employeeRole,
            trainerTypeEnum, isActive);
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

    public async Task<Result<PersonForEditResponse>> GetPersonForEditAsync(Guid personId)
    {
        Person? person = await _personRepo.GetPersonByIdAsync(personId);
        if (person == null)
        {
            return Result<PersonForEditResponse>.Failure("Person not found!", StatusCodeEnum.NotFound);
        }

        PersonForEditResponse personForEdit = new PersonForEditResponse()
        {
            City = person.City,
            LastName = person.LastName,
            PhoneNumber = person.PhoneNumber,
            Street = person.Street,
        };

        return Result<PersonForEditResponse>.Success(personForEdit, StatusCodeEnum.NotFound);
    }

    public async Task<Result<Unit>> UpdatePersonAsync(PersonUpdateRequest request)
    {
        Person? person = await _personRepo.GetPersonByIdAsync(request.PersonId);
        if (person == null)
        {
            return Result<Unit>.Failure("Person not found!", StatusCodeEnum.NotFound);
        }
        bool exists = await _personRepo.ExistsByPhoneAsync(request.PhoneNumber, request.PersonId);
        if(exists)
        {
            return Result<Unit>.Failure("Person from the staff with the same phone number already exists!", StatusCodeEnum.Conflict);
        }

        bool clientExists = await _clientRepo.ExistsByPhoneAsync(request.PhoneNumber, null);
        if (clientExists)
        {
            return Result<Unit>.Failure("Client with the same phone number already exists!", StatusCodeEnum.Conflict);
        }

        person.ModifyPerson(request);
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }
}
