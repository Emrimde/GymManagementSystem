using GymManagementSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IPersonalBookingRepository
{
    Task<IEnumerable<PersonalBooking>> GetForRangeAsync(Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct);
    Task<PersonalBooking> AddAsync(PersonalBooking entity);
    Task<bool> CancelAsync(Guid bookingId, CancellationToken ct);
}
