namespace GymManagementSystem.Core.DTO.Email;
public class EmailRequest
{
    public string To { get; set; } = default!;
    public string? Subject { get; set; }
    public string? Body { get; set; } = default!;
}
