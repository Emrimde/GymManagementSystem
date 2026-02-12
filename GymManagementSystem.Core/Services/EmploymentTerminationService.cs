using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain;
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
    private readonly IUnitOfWork _unitOfWork;

    public EmploymentTerminationService(IEmploymentTerminationRepository employmentTerminationRepository, IPersonRepository personRepo, IUnitOfWork unitOfWork)
    {
        _employmentTerminationRepository = employmentTerminationRepository;
        _personRepo = personRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> CreateEmploymentTerminationAsync(EmploymentTerminationAddRequest request)
    {
        EmploymentTermination employmentTermination = request.ToEmploymentTermination();
        _employmentTerminationRepository.AddEmploymentTermination(employmentTermination);

        Person? person = await _personRepo.GetPersonByIdAsync(request.PersonId);
        if (person == null)
        {
            return Result<Unit>.Failure("Person not found", StatusCodeEnum.NotFound);
        }
        if (person.Employee != null)
        {
            person.Employee.ValidTo = request.EffectiveDate;
        }
        else if (person.TrainerContract != null)
        {
            person.TrainerContract.ValidTo = request.EffectiveDate;
        }
        else
        {
            return Result<Unit>.Failure("Unexpected error", StatusCodeEnum.InternalServerError);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Created);
    }

    public async Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(Guid personId)
    {
        Person? person = await _personRepo.GetPersonByIdAsync(personId);

        if (person is null)
        {
            return Result<EmploymentTerminationGenerateResponse>.Failure("Person not found", StatusCodeEnum.NotFound);
        }

        EmploymentTermination termination = new EmploymentTermination();
        termination.PersonId = person.Id;

        if (person.Employee != null)
        {
            var validFrom = person.Employee.ValidFrom.Date;

            int months =
                ((DateTime.UtcNow.Year - validFrom.Year) * 12) +
                (DateTime.UtcNow.Month - validFrom.Month);

            termination.RequestedDate = DateTime.UtcNow.Date;
            if (validFrom.Day > DateTime.UtcNow.Day)
            {
                months--;
            }
            if (months < 6)
            {
                termination.EffectiveDate = DateTime.UtcNow.Date.AddDays(14);
            }
            else if (months < 36)
            {
                termination.EffectiveDate = DateTime.UtcNow.Date.AddDays(1);
            }
            else
            {
                termination.EffectiveDate = DateTime.UtcNow.Date.AddDays(3);
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
