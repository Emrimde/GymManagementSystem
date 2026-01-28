using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IGeneralGymRepository _generalGymRepository;
    private readonly IPersonRepository _personRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    public EmployeeService(IEmployeeRepository employeeRepo, UserManager<User> userManager, IPersonRepository personRepo, IGeneralGymRepository generalGymRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepo = employeeRepo;
        _userManager = userManager;
        _personRepo = personRepo;
        _generalGymRepository = generalGymRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<Result<EmployeeInfoResponse>> CreateEmployeeAsync(EmployeeAddRequest request)
    {
        Employee employee = request.ToEmployee();
        employee.ContractTypeEnum = ContractTypeEnum.Permanent;

        Person? person = await _personRepo.GetPersonByIdAsync(employee.PersonId);
        if (person == null)
        {
            return Result<EmployeeInfoResponse>.Failure("Unexpected error during searching person data", StatusCodeEnum.InternalServerError);
        }

        person.IsActive = true;

        User user = new User()
        {
            UserName = $"{person.FirstName + person.LastName}",
        };
        var createResult = await _userManager.CreateAsync(user, "employee");
        if (!createResult.Succeeded)
        {
            return Result<EmployeeInfoResponse>.Failure($"{createResult.Errors}", StatusCodeEnum.InternalServerError);
        }
        await _userManager.AddToRoleAsync(user, "Receptionist");

        _employeeRepo.CreateEmployee(employee);
        await _unitOfWork.SaveChangesAsync();
        EmployeeInfoResponse response = employee.ToEmployeeInfoResponse();

        return Result<EmployeeInfoResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync(string? searchText = null)
    {
        IEnumerable<Employee> employees = await _employeeRepo.GetAllEmployeesAsync(searchText);
        return Result<IEnumerable<EmployeeResponse>>.Success(employees.Select(item => item.ToEmployeeResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<EmployeeDetailsResponse>> GetEmployeeByIdAsync(Guid employeeId)
    {
        Employee? employee = await _employeeRepo.GetEmployeeByIdAsync(employeeId);
        if (employee == null)
        {
            Result<EmployeeDetailsResponse>.Failure("Employee not found", StatusCodeEnum.NotFound);
        }
        return Result<EmployeeDetailsResponse>.Success(employee!.ToEmployeeDetailsResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<EmploymentContractPdfDto>> BuildEmployeeContractAsync(EmployeeContractRequest request)
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync();
        if (generalGymDetail == null)
        {
            return Result<EmploymentContractPdfDto>.Failure("Gym details not found", StatusCodeEnum.NotFound);
        }

        Person? person = await _personRepo.GetPersonByIdAsync(request.PersonId);
        if (person == null)
        {
            return Result<EmploymentContractPdfDto>.Failure("Person not found", StatusCodeEnum.NotFound);
        }



        EmploymentContractPdfDto employmentContractPdfDto = new EmploymentContractPdfDto()
        {
            ContractType = ContractTypeEnum.Permanent,
            EmploymentType = request.EmploymentType,
            Salary = request.MonthlySalaryBrutto.ToString(),
            Role = request.Role,
            ContactNumber = generalGymDetail.ContactNumber,
            GymAddress = generalGymDetail.Address,
            GymName = generalGymDetail.GymName,
            Nip = generalGymDetail.Nip,
            ValidTo = "indefinite time",
            ValidFrom = DateTime.UtcNow.Date.ToString("dd.MM.yyyy"),
            Address = person.Street + ", " + person.City,
            Email = person.Email,
            FirstName = person.FirstName,
            LastName = person.LastName,
            PhoneNumber = person.PhoneNumber
        };

        return Result<EmploymentContractPdfDto>.Success(employmentContractPdfDto, StatusCodeEnum.Ok);
    }
}
