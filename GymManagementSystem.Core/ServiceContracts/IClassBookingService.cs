using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IClassBookingService
{
    Task<Result<IEnumerable<ClassBookingResponse>>> GetAllByClientIdAsync(Guid? clientId);
    Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request);
    Task<Result<ClassBookingDetailsResponse>> GetByIdAsync( Guid id);
    Task<Result<Unit>> DeleteClassBookingAsync(Guid classBookingId);
}
