namespace GymManagementSystem.Core.WebDTO.TrainerTimeOff;

public class TrainerTimeOffWebResponse
{
    public string TimeOffStart { get; set; } = default!;
    public string TimeOffEnd { get; set; } = default!;
    public string TimeOffStartDate { get; set; } = default!;    
    public string TimeOffEndDate { get; set; } = default!;
    public string? Reason { get; set; } 
}
