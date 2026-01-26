using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.Person.ReadModel;

namespace GymManagementSystem.Core.Mappers;
public static class PersonMapper
{
    public static PersonResponse ToPersonResponse(this PersonReadModel personReadModel)
    {
        return new PersonResponse()
        {
            
            CreatedAt = personReadModel.CreatedAt.ToString("dd.MM.yyyy"),
            Email = personReadModel.Email,
            FirstName = personReadModel.FirstName,
            LastName = personReadModel.LastName,
            Id = personReadModel.Id,
            //Role = personReadModel.EmployeeRole.ToString() ?? personReadModel.TrainerTypeEnum.ToString() ?? "No role",
            Role = personReadModel.HasEmployee ? personReadModel.EmployeeRole.ToString() : personReadModel.HasTrainer ? personReadModel.TrainerTypeEnum.ToString() : "No role",
            EmployeeId = personReadModel.EmployeeId,
            TrainerContractId = personReadModel.TrainerContractId,
            IsActive = personReadModel.IsActive,
            PhoneNumber = personReadModel.PhoneNumber,
            UpdatedAt = personReadModel.UpdatedAt.ToString("dd.MM.yyyy")
        };
    }
    public static PersonDetailsResponse ToPersonDetailsResponse(this Person person)
    {
        return new PersonDetailsResponse()
        {
            Email = person.Email,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Id = person.Id,
            Address = person.Street + ", " + person.City,
            //Role = personReadModel.EmployeeRole.ToString() ?? personReadModel.TrainerTypeEnum.ToString() ?? "No role",
            //Role = person.Employee != null ? person.Employee.Role.ToString() : person.TrainerContract != null ? person.TrainerContract.TrainerType.ToString() : "No role",
            Role = "No role",
            //Status = person.IsActive ? "Active" : "Unactive",
            PhoneNumber = person.PhoneNumber,
            //ValidFrom = person.Employee != null ? person.Employee.ValidFrom.ToString("dd.MM.yyyy") : person.TrainerContract != null ? person.TrainerContract.ValidFrom.ToString("dd.MM.yyyy") : " - ",
            //ValidTo = person.Employee != null ? person.Employee.ValidTo?.ToString("dd.MM.yyyy") ?? "indefinite" : person.TrainerContract != null ? person.TrainerContract.ValidTo?.ToString("dd.MM.yyyy") ?? "indefinite" : " - ",
            IsActive = person.IsActive
        };
    }
    public static Person ToPerson(this PersonAddRequest personAddRequest)
    {
        return new Person()
        {
            Email = personAddRequest.Email,
            FirstName = personAddRequest.FirstName,
            LastName = personAddRequest.LastName,
            PhoneNumber = personAddRequest.PhoneNumber,
            Street = personAddRequest.Street,
            City = personAddRequest.City,
        };
    }
    public static void ModifyPerson(this Person person, PersonUpdateRequest request)
    {
        person.LastName = request.LastName;
        person.PhoneNumber = request.PhoneNumber;
        person.Street = request.Street;
        person.City = request.City;
    }
}
