using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClassBookingRepository : IRepository<ClassBookingResponse, ClassBooking>
{
    Task<bool> DeleteClassBookingAsync(Guid classBookingId);
    Task<IEnumerable<ClassBookingResponse>> GetAllClassBookingsByClientId(Guid clientId);
    Task DeleteClassBookingsByGymClassIdAsync(Guid gymClassId);
}
