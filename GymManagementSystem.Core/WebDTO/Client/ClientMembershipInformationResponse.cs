namespace GymManagementSystem.Core.WebDTO.Client;
public class ClientMembershipInformationResponse
{
    public bool HasActiveMembership { get; set; }
    public string? StartDate { get; set; } = default!;
    public string? EndDate { get; set; } = default!;
}
