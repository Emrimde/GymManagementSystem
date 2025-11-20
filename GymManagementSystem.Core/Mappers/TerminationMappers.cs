using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Mappers.ClientMapper;

namespace GymManagementSystem.Core.Mappers;

public static class TerminationMappers
{
    public static Termination ToTermination(this TerminationAddRequest request)
    {
        return new Termination()
        {
            ClientId = request.ClientId,
            ContractId = request.ContractId,
            Reason = request.Reason,
            RequestedAt = DateTime.UtcNow,
        };
    }
    public static TerminationResponse ToTerminationResponse(this Termination termination)
    {
        return new TerminationResponse()
        {

            Reason = termination.Reason,
            RequestedAt = termination.RequestedAt.ToString("dd.MM.yyyy"),
            FirstName = termination.Client != null ? termination.Client.FirstName : "error",
            LastName = termination.Client != null ? termination.Client.LastName: "error",
        };
    }
}
