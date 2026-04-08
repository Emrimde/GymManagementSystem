using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.QueryContracts;
public interface IGymClassQueryService
{
    Task<IEnumerable<GymClassResponse>> GetGymClassesResponseAsync(bool? isActive);
    Task<IEnumerable<GymClassComboBoxResponse>> GetGymClassComboBoxResponseAsync();
    Task<GymClassForEditResponse?> GetGymClassForEditAsync(Guid gymClassId);
}
