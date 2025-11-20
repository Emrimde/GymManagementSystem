using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class GeneralGymDetailsRepository : IGeneralGymRepository
{
    private readonly ApplicationDbContext _dbContext;
    public GeneralGymDetailsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GeneralGymDetail?> GetGeneralGymDetailsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.GeneralGymDetails.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GeneralGymDetail?> UpdateSettingsAsync(GeneralGymUpdateRequest updatedGeneralSettings, CancellationToken cancellationToken)
    {
        GeneralGymDetail? generalSettings = await _dbContext.GeneralGymDetails.FirstOrDefaultAsync(cancellationToken);

        if (generalSettings == null)
        {
            return null!;
        }

        generalSettings.GymName = updatedGeneralSettings.GymName;
        generalSettings.ContactNumber = updatedGeneralSettings.ContactNumber;
        generalSettings.Address = updatedGeneralSettings.Address;
        generalSettings.BackgroundColor = updatedGeneralSettings.BackgroundColor;
        generalSettings.PrimaryColor = updatedGeneralSettings.PrimaryColor;
        generalSettings.SecondColor = updatedGeneralSettings.SecondColor;
        int modified = await _dbContext.SaveChangesAsync(cancellationToken);

        if (modified < 0)
        {
            return null;
        }
        return generalSettings;
    }
}
