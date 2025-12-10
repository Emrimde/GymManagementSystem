using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IClassBookingService
{
    Task<PageResult<ClassBookingResponse>> GetAllAsync();
    Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request);
    Task<Result<ClassBookingDetailsResponse>> GetByIdAsync( Guid id);
}
