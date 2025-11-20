using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IGeneralGymRepository
{
    Task<GeneralGymDetail?> GetGeneralGymDetailsAsync(CancellationToken cancellationToken);
    Task<GeneralGymDetail> UpdateSettingsAsync(DTO.GeneralGymDetail.GeneralGymUpdateRequest request, CancellationToken cancellationToken);
}
