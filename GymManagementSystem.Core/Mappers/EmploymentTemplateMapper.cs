using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTemplate;

namespace GymManagementSystem.Core.Mappers;

public static class EmploymentTemplateMapper
{
    public static EmploymentTemplateInfoResponse ToEmploymentTemplateInfoResponse(this EmploymentTemplate employmentTemplate)
    {
        return new EmploymentTemplateInfoResponse()
        {
            Id = employmentTemplate.Id,
        };
    }
    public static EmploymentTemplateResponse ToEmploymentTemplateResponse(this EmploymentTemplate employmentTemplate) 
    {
        return new EmploymentTemplateResponse()
        {
            Id = employmentTemplate.Id,
            EmploymentType = employmentTemplate.EmploymentType,
            MonthlySalary = employmentTemplate.MonthlySalary,
            HourlyRate = employmentTemplate.HourlyRate,
            NetRate = employmentTemplate.NetRate,
        };
    }
    public static EmploymentTemplate ToEmploymentTemplate(this EmploymentTemplateAddRequest employmentTemplate) 
    {
        return new EmploymentTemplate()
        {
            EmploymentType = employmentTemplate.EmploymentType,
            MonthlySalary = employmentTemplate.MonthlySalary,
            HourlyRate = employmentTemplate.HourlyRate,
            NetRate = employmentTemplate.NetRate,
        };
    }
}
