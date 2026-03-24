using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClassBookingRepository : IRepository<ClassBookingResponse, ClassBooking>
{
    Task<int> CountClassBookingsByScheduledClassId(Guid scheduleClassId);
    Task<bool> DeleteClassBookingAsync(Guid classBookingId);
    Task<IEnumerable<ClassBookingResponse>> GetAllClassBookingsByClientId(Guid clientId);
    void DeleteClassBookingList(IEnumerable<ClassBooking> entity);
}
