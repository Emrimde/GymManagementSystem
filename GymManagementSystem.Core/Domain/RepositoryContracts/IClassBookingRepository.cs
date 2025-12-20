using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClassBookingRepository : IRepository<ClassBookingResponse, ClassBooking>
{
   Task<int> CountClassBookingsByScheduledClassId(Guid scheduleClassId);
   Task<IEnumerable<ClassBookingReadModel>> GetAllClassBookingsByClientId(Guid clientId);
}
