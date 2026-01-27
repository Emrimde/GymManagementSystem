using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;
public class PersonStatusService : IPersonStatusService
{
    private readonly IPersonRepository _personRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IEmploymentTerminationRepository _employmentTerminationRepository;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public PersonStatusService(IPersonRepository personRepository, IUnitOfWork unitOfWork, ITrainerRepository trainerRepository, IEmployeeRepository employeeRepository, UserManager<User> userManager, IEmploymentTerminationRepository employmentTerminationRepository)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _trainerRepository = trainerRepository;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _employmentTerminationRepository = employmentTerminationRepository;
    }

    public async Task DeactivateExpiredAsync()
    {
        IEnumerable<Person> people = await _personRepository.GetAllActivePeopleWithTerminationAsync();
        foreach (Person person in people)
        {
            Employee? employee = person.Employee;
            TrainerContract? trainerContract = person.TrainerContract;
            person.IsActive = false;
            person.Employee = null;
            person.TrainerContract = null;
            if (employee != null)
            {
                _employeeRepository.DeleteEmployee(employee);
            }

            else if (trainerContract != null)
            {
                _trainerRepository.DeleteTrainer(trainerContract);
            }
            if (person.IdentityUserId != null)
            {
                User? user = await _userManager.FindByIdAsync(person.IdentityUserId.Value.ToString());
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
                person.IdentityUserId = null;
                person.UpdatedAt = DateTime.UtcNow;
            }

            EmploymentTermination? employmentTermination = await _employmentTerminationRepository.GetActiveEmploymentTerminationByPersonId(person.Id);
            if (employmentTermination != null)
            {
                employmentTermination!.IsActive = false;
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }
}

