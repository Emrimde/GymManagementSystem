using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Feature;

namespace GymManagementSystem.Core.Mappers.ClientMapper;
public static class FeatureMapper
{
    public static FeatureSelectResponse ToFeatureSelectResponse(this Feature feature)
    {
        return new FeatureSelectResponse()
        {
            BenefitDescription = feature.BenefitDescription,
            FeatureId = feature.Id,
        };
    }
}
