using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IClassBookingService
{
    Task<Result<IEnumerable<ClassBookingResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request);
    Task<Result<ClassBookingDetailsResponse>> GetByIdAsync( Guid id, CancellationToken cancellationToken);
}
