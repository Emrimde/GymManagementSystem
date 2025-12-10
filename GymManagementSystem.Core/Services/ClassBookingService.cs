using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClassBookingService : IClassBookingService
{
    private readonly IRepository<ClassBookingResponse,ClassBooking> _classBookingRepo;
    public ClassBookingService(IRepository<ClassBookingResponse,ClassBooking> classBookingRepo)
    {
        _classBookingRepo = classBookingRepo;
    }
    public async Task<Result<ClassBookingInfoResponse>> CreateAsync(ClassBookingAddRequest request)
    {
        ClassBooking addedClassBooking = await _classBookingRepo.CreateAsync(request.ToClassBooking());
        return Result<ClassBookingInfoResponse>.Success(addedClassBooking.ToClassBookingInfo());
    }

    public async Task<PageResult<ClassBookingResponse>> GetAllAsync()
    {
        PageResult<ClassBookingResponse> classBookings = await _classBookingRepo.GetAllAsync();
        return classBookings;
    }

    public Task<Result<ClassBookingDetailsResponse>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
