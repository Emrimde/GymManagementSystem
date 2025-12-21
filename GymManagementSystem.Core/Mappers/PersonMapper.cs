using GymManagementSystem.Core.Domain.Entities;
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
            IsActive = personReadModel.IsActive,
            PhoneNumber = personReadModel.PhoneNumber,
            UpdatedAt = personReadModel.UpdatedAt.ToString("dd.MM.yyyy")
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
        };
    }
}
