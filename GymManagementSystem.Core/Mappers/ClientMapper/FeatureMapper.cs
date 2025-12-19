using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Feature;

namespace GymManagementSystem.Core.Mappers.ClientMapper;
public static class FeatureMapper
{
    public static FeatureResponse ToFeatureSelectResponse(this Feature feature)
    {
        return new FeatureResponse()
        {
            BenefitDescription = feature.BenefitDescription,
            FeatureId = feature.Id,
        };
    }
}
