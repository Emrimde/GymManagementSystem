using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class TrainerTimeOffRepository : ITrainerTimeOffRepository
{
    private readonly ApplicationDbContext _db;
    public TrainerTimeOffRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TrainerTimeOff>> GetForRangeAsync(
       Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var start = DateTime.SpecifyKind(
            from.ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        var end = DateTime.SpecifyKind(
            to.AddDays(1).ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        return await _db.TrainerTimeOff
            .AsNoTracking()
            .Where(t => t.TrainerId == trainerId &&
                        t.Start >= start &&
                        t.Start < end)
            .ToListAsync(ct);
    }



    public async Task<TrainerTimeOff> AddAsync(TrainerTimeOff entity, CancellationToken ct)
    {
        await _db.TrainerTimeOff.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var off = await _db.TrainerTimeOff.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (off == null)
            return false;

        _db.TrainerTimeOff.Remove(off);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
