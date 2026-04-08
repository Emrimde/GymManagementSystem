using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.API.Controllers;
public interface IGymClassService
{
    Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest entity);
    Task<Result<Unit>> DeleteGymClassAsync(Guid gymClassId);
    Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(bool? isActive);
    Task<Result<IEnumerable<GymClassComboBoxResponse>>> GetGymClassesForSelectAsync();
    Task<Result<GymClassForEditResponse>> GetGymClassForEditAsync(Guid gymClassId);
    Task<Result<Unit>> RestoreGymClassAsync(Guid gymClassId);
    Task<Result<Unit>> UpdateAsync(GymClassUpdateRequest entity);
}