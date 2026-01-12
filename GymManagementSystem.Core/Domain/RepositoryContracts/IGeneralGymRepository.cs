using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GeneralGymDetail;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IGeneralGymRepository
{
    Task<GeneralGymDetail?> GetGeneralGymDetailsAsync();
}
