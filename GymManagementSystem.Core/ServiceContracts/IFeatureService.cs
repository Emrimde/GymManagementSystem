using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IFeatureService
{
    Task<Result<IEnumerable<FeatureSelectResponse>>> GetFeaturesForSelect();
}
