using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.Person.ReadModel;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IPersonRepository
{
    Task<Person?> GetPersonByIdAsync(Guid personId);
    Task<IEnumerable<PersonReadModel>> GetAllStaffAsync();
    void AddPersonToStaff(Person person);
    void UpdatePerson(Person person);
}
