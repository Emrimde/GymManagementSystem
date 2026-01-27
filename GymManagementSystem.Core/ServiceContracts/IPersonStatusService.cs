namespace GymManagementSystem.Core.ServiceContracts;
public interface IPersonStatusService
{
    Task DeactivateExpiredAsync();
}
