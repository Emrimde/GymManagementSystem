using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;
public class EmploymentTemplateService : IEmploymentTemplateService
{
    private readonly IEmploymentTemplateRepository _employmentTemplateRepo;
    public EmploymentTemplateService(IEmploymentTemplateRepository employmentTemplateRepository)
    {
        _employmentTemplateRepo = employmentTemplateRepository;
    }
    public async Task<Result<EmploymentTemplateInfoResponse>> CreateEmploymentTemplateAsync(EmploymentTemplateAddRequest request)
    {
        EmploymentTemplateInfoResponse response = await _employmentTemplateRepo.CreateEmploymentTemplateAsync(request.ToEmploymentTemplate());
        return Result<EmploymentTemplateInfoResponse>.Success(response);
    }

    public async Task<Result<IEnumerable<EmploymentTemplateResponse>>> GetAllEmploymentTemplatesAsync(CancellationToken cancellationToken)
    {
        IEnumerable<EmploymentTemplate> response = await _employmentTemplateRepo.GetAllEmploymentTemplateAsync(cancellationToken);
        return Result<IEnumerable<EmploymentTemplateResponse>>.Success(response.Select(item => item.ToEmploymentTemplateResponse()));
    }
}
