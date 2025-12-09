using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Services;

public class EmploymentTerminationService : IEmploymentTerminationService
{
    private readonly IEmploymentTerminationRepository _employmentTerminationRepository;
    private readonly IPersonRepository _personRepo;
   
    public EmploymentTerminationService(IEmploymentTerminationRepository employmentTerminationRepository, IPersonRepository personRepo)
    {
        _employmentTerminationRepository = employmentTerminationRepository;
        _personRepo = personRepo;
    }

    public async Task<Result<EmploymentTerminationInfoResponse>> CreateEmploymentTerminationAsync(EmploymentTerminationAddRequest request)
    {
        EmploymentTerminationInfoResponse employment =  await _employmentTerminationRepository.AddEmploymentTermination(request.ToEmploymentTermination());
        return Result<EmploymentTerminationInfoResponse>.Success(employment, StatusCodeEnum.Created);
    }

    public async Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(Guid personId)
    {
        Person? person = await _personRepo.GetPersonByIdAsync(personId);
        
        if(person is null)
        {
            return Result<EmploymentTerminationGenerateResponse>.Failure("Person not found", StatusCodeEnum.NotFound);
        }
        
        EmploymentTermination termination = new EmploymentTermination();
        termination.PersonId = person.Id;

        if (person.Employee != null)
        {
            var validFrom = person.Employee.ValidFrom;

            int months =
                ((DateTime.UtcNow.Year - validFrom.Year) * 12) +
                (DateTime.UtcNow.Month - validFrom.Month);

            termination.RequestedDate = DateTime.UtcNow;
            if (validFrom.Day > DateTime.UtcNow.Day)
            {
                months--; // odejmujemy niepełny miesiąc
            }
            if (months < 6)
            {
                termination.EffectiveDate = DateTime.UtcNow.AddDays(14);
            }
            else if (months < 36)
            {
                termination.EffectiveDate = DateTime.UtcNow.AddMonths(1);
            }
            else
            {
                termination.EffectiveDate = DateTime.UtcNow.AddMonths(3);
            }
            
        }

        else if (person.TrainerContract != null)
        {
            termination.RequestedDate = DateTime.UtcNow;
            termination.EffectiveDate = DateTime.UtcNow.AddDays(14);
        }

        else
        {
            return Result<EmploymentTerminationGenerateResponse>.Failure("Person does not have an employment or trainer contract", StatusCodeEnum.BadRequest);
        }
        
        return Result<EmploymentTerminationGenerateResponse>.Success(termination.ToEmploymentGenerateResponseTermination(person), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<EmploymentTerminationResponse>>> GetEmploymentTerminationsAsync()
    {
        IEnumerable<EmploymentTerminationResponse> terminations = await _employmentTerminationRepository.GetEmploymentTerminationsAsync();
        return Result<IEnumerable<EmploymentTerminationResponse>>.Success(terminations, StatusCodeEnum.Ok);
    }
}
