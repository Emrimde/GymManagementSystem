using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTermination;

namespace GymManagementSystem.Core.Mappers;
public static class EmploymentTerminationMapper
{
    public static EmploymentTerminationInfoResponse ToEmploymentTerminationInfoResponse(this EmploymentTermination entity)
    {
        return new EmploymentTerminationInfoResponse
        {
            Id = entity.Id
        };
    }

    public static EmploymentTermination ToEmploymentTermination(this EmploymentTerminationAddRequest request)
    {
        return new EmploymentTermination
        {
            PersonId = request.PersonId,
            EffectiveDate = request.EffectiveDate
        };
    }
    public static EmploymentTerminationGenerateResponse ToEmploymentGenerateResponseTermination(this EmploymentTermination request, Person person)
    {
        return new EmploymentTerminationGenerateResponse
        {
            ContractType = person.Employee?.ContractTypeEnum.ToString() ?? person.TrainerContract?.ContractType.ToString() ?? string.Empty,
            EmploymentType = person.Employee?.EmploymentType.ToString() ?? string.Empty,
            FirstName = person.FirstName ?? string.Empty,
            LastName = person.LastName ?? string.Empty,
            EffectiveDate = request.EffectiveDate,
            RequestedDate = request.RequestedDate
        };
    }
}
