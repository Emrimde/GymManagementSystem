namespace GymManagementSystem.Core.DTO.TrainerContract;
public class TrainerContractCreatedResponse
{
    public Guid TrainerContractId { get; set; }
    public string TemporaryPassword { get; set; } = default!;
}
