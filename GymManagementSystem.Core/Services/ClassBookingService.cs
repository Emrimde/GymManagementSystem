using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClassBookingService : IClassBookingService
{
    private readonly IRepository<ClassBooking> _classBookingRepo;
    public ClassBookingService(IRepository<ClassBooking> classBookingRepo)
    {
        _classBookingRepo = classBookingRepo;
    }
    public async Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request, CancellationToken cancellationToken)
    {
        ClassBooking addedClassBooking = await _classBookingRepo.CreateAsync(request.ToClassBooking(), cancellationToken);
        return Result<ClassBookingInfoResponse>.Success(addedClassBooking.ToClassBookingInfo());
    }

    public async Task<Result<IEnumerable<ClassBookingResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<ClassBooking> classBookings = await _classBookingRepo.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ClassBookingResponse>>.Success(classBookings.Select(item => item.ToClassBookingResponse()));
    }

    public Task<Result<ClassBookingDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
