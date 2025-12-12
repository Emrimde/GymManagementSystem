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
            ClientMembershipId = request.ClientMembershipId,
            //ContractId = request.ContractId,
            Reason = request.Reason,
        };
    }
    public static TerminationResponse ToTerminationResponse(this Termination termination)
    {
        return new TerminationResponse()
        {

            Reason = termination.Reason,
            RequestedAt = termination.RequestedAt.ToString("dd.MM.yyyy"),
            //FirstName = termination.ClientMembership != null ? termination.ClientMembership.FirstName : "error",
            //LastName = termination.ClientMembership != null ? termination.ClientMembership.LastName: "error",
        };
    }
}
