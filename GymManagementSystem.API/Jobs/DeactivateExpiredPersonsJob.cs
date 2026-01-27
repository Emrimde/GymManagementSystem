using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.API.Jobs;
public class DeactivateExpiredPersonsJob
{
    private readonly IPersonStatusService _personStatusService;
    public DeactivateExpiredPersonsJob(IPersonStatusService personStatusService)
    {
        _personStatusService = personStatusService;
    }

    public Task Run()
        => _personStatusService.DeactivateExpiredAsync();
}
