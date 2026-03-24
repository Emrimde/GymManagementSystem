using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IClassBookingService
{
    Task<Result<IEnumerable<ClassBookingResponse>>> GetAllClassBookingsByClientIdAsync(Guid? clientId);
    Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request);
    Task<Result<Unit>> DeleteClassBookingAsync(Guid classBookingId);
}
