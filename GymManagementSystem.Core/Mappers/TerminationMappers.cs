using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Mappers.ClientMapper;

namespace GymManagementSystem.Core.Mappers;

public static class TerminationMappers
{
    public static Termination ToTermination(this TerminationAddRequest request,Guid clientMembershipId)
    {
        return new Termination()
        {
            ClientMembershipId = clientMembershipId,
            Reason = request.Reason,
        };
    }
    public static TerminationResponse ToTerminationResponse(this Termination termination)
    {
        return new TerminationResponse()
        {

            Reason = termination.Reason,
            RequestedAt = termination.RequestedAt.ToString("dd.MM.yyyy"),
        };
    }
}
