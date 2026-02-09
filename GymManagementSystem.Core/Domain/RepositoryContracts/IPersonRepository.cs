using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.Person.ReadModel;
using GymManagementSystem.Core.WebDTO.Trainer;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IPersonRepository
{
    Task<Person?> GetPersonByIdAsync(Guid personId);
    Task<IEnumerable<PersonReadModel>> GetAllStaffAsync(string? searchText, bool? isTrainer, Enum.EmployeeRole? employeeRole, Enum.TrainerTypeEnum? trainerTypeEnum, bool? isActive);
    void AddPersonToStaff(Person person);
    void UpdatePerson(Person person);
    Task<IEnumerable<Person>> GetAllActivePeopleWithTerminationAsync();
    Task<GroupInstructorPanelResponse?> GetGroupInstructorPanelResponseAsync(Guid personId);
}
