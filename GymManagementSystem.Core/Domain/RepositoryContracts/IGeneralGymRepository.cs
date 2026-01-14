using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IGeneralGymRepository
{
    Task<GeneralGymDetail?> GetGeneralGymDetailsAsync();
}
