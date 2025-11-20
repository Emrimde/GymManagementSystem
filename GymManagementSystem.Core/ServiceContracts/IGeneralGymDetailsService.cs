using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Result;


namespace GymManagementSystem.Core.ServiceContracts;

public interface IGeneralGymDetailsService
{
    Task<Result<GeneralGymResponse>> GetSettingsByIdAsync(CancellationToken cancellationToken);
    Task<Result<GeneralGymResponse>> UpdateSettingsAsync(GeneralGymUpdateRequest request, CancellationToken cancellationToken);
}
