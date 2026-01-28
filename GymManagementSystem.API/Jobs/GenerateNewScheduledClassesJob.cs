using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.API.Jobs;
public class GenerateNewScheduledClassesJob
{
    private readonly IScheduleGeneratorService _scheduleGeneratorService;
    public GenerateNewScheduledClassesJob(IScheduleGeneratorService scheduleGeneratorService)
    {
        _scheduleGeneratorService = scheduleGeneratorService;
    }

    public Task Run()
        => _scheduleGeneratorService.GenerateNewScheduledClassesAsync();
}
