using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.ViewModels.Staff.Enum;
using GymManagementSystem.WPF.ViewModels.Staff.Models;

namespace GymManagementSystem.WPF.ViewModels.Staff.Helper;
public static class StatusFilterHelper
{
    public static StaffFilterRequest? BuildFilterRequest(StaffFilterType selectedFilterType)
    {
        return selectedFilterType switch
        {
            StaffFilterType.All => new(),
            StaffFilterType.Employee => new()
            {
                IsTrainer = false,
            },
            StaffFilterType.Trainer => new()
            {
                IsTrainer = true,
            },
            StaffFilterType.GroupTrainer => new()
            {
                IsTrainer = true,
                TrainerType = TrainerTypeEnum.GroupInstructor
            },
            StaffFilterType.PersonalTrainer => new()
            {
                IsTrainer = true,
                TrainerType = TrainerTypeEnum.PersonalTrainer
            },
            StaffFilterType.Manager => new()
            {
                IsTrainer = false,
                EmployeeRole = EmployeeRole.Manager,
            },
            StaffFilterType.Receptionist => new()
            {
                IsTrainer = false,
                EmployeeRole = EmployeeRole.Receptionist,
            },
            _ => null
        };
    }
}
