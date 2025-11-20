using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Mappers.ClientMapper;

namespace GymManagementSystem.Core.Mappers;

public static class ContractMapper
{
    public static ContractResponse ToContractResponse(this Contract contract)
    {
        return new ContractResponse()
        {
            ClientMembership = contract.ClientMembership != null ? contract.ClientMembership.ToClientMembershipResponse() : null,
            CreatedAt = contract.CreatedAt.ToString("dd:MM:yyyy"),
            Id = contract.Id,
            ContractStatus = contract.ContractStatus
        };
    }

    public static Contract ToContract(this ContractUpdateRequest contract)
    {
        return new Contract()
        {
            ContractStatus = contract.ContractStatus,
        };
    }

    public static ContractDetailsResponse ToContractDetails(this Contract contract)
    {
        return new ContractDetailsResponse()
        {
            Client = contract.ClientMembership?.Client?.ToClientDetailsResponse(),
            Name = contract.ClientMembership?.Membership?.Name + " " + contract.ClientMembership?.Membership?.MembershipType,
            Price = (decimal)contract.ClientMembership?.Membership?.Price!,
            StartDate = contract.ClientMembership.StartDate.ToString("dd.MM.yyyy"),
            EndDate = contract.ClientMembership.EndDate?.ToString("dd.MM.yyyy")
          ?? "indefinite time",
            MembershipType = contract.ClientMembership.Membership.MembershipType,
            ContractStatus = contract.ContractStatus
        };
    }
}
