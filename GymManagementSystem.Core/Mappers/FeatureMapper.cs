using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Feature;

namespace GymManagementSystem.Core.Mappers;
public static class FeatureMapper
{
    public static Feature ToFeature(this FeatureAddRequest feature)
    {
        return new Feature()
        {
            BenefitDescription = feature.BenefitDescription
        };
    }
}
