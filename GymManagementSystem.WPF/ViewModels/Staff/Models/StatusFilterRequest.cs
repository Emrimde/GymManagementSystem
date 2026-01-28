using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.WPF.ViewModels.Staff.Models;

public class StaffFilterRequest
{
    public bool? IsTrainer { get; set; }
    public EmployeeRole? EmployeeRole { get; set; }
    public TrainerTypeEnum? TrainerType { get; set; }
}
