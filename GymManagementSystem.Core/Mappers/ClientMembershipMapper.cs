using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Mappers.ClientMapper;

namespace GymManagementSystem.Core.Mappers;
public static class ClientMembershipMapper
{
    public static ClientMembership ToClientMembership(this ClientMembershipAddRequest request)
    {
        return new ClientMembership()
        {
            ClientId = request.ClientId,
            MembershipId = request.MembershipId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
    }

    public static ClientMembershipResponse ToClientMembershipResponse(this ClientMembership request)
    {
        return new ClientMembershipResponse()
        {
            Id = request.Id,
            Client = request.Client != null ? request.Client.ToClientResponse() : null,
            Membership = request.Membership != null ? request.Membership.ToMembershipResponse() : null,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
    }

    public static ClientMembershipShortResponse ToClientMembershipShortResponse(this ClientMembership request)
    {
        return new ClientMembershipShortResponse()
        {
            Membership = request.Membership != null ? request.Membership.ToMembershipResponse() : null,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive,
            ContractId = request.Contract != null && request.Contract.IsActive ? request.Contract.Id : Guid.Empty,
        };
    }



    public static ClientMembershipInfoResponse ToClientMembershipInfoResponse(this ClientMembership request,Guid id) 
    {
        return new ClientMembershipInfoResponse()
        {
            Id = request.Id,
            ContractId = id
        };
    }
}
