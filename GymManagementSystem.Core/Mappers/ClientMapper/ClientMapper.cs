using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Mappers.ClientMapper;

public static class ClientMapper
{
    public static ClientResponse ToClientResponse(this Client client)
    {
        return new ClientResponse
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            DateOfBirth = client.DateOfBirth,
            Street = client.StreetAddress,
            City = client.City,
        };
    }
    public static Client ToClient(this ClientUpdateRequest clientUpdateRequest)
    {
        return new Client
        {
            FirstName = clientUpdateRequest.FirstName,
            LastName = clientUpdateRequest.LastName,
            Email = clientUpdateRequest.Email,
            PhoneNumber = clientUpdateRequest.PhoneNumber,
            DateOfBirth = clientUpdateRequest.DateOfBirth,
            StreetAddress = clientUpdateRequest.Street,
           
            City = clientUpdateRequest.City,
           
        };
    }
    public static Client ToClient(this ClientAddRequest clientAddRequest)
    {
        return new Client
        {
            FirstName = clientAddRequest.FirstName,
            LastName = clientAddRequest.LastName,
            Email = clientAddRequest.Email,
            PhoneNumber = clientAddRequest.PhoneNumber,
            DateOfBirth = clientAddRequest.DateOfBirth,
            StreetAddress = clientAddRequest.Street,
            City = clientAddRequest.City,
        };
    }

    public static ClientInfoResponse ToClientInfoResponse(this Client client)
    {
        return new ClientInfoResponse
        {
            Id = client.Id,
            FullName = client.FirstName + " " + client.LastName,
        };
    }

    public static void ModifyClient(this Client client, Client modifiedClient)
    {

        client.FirstName = modifiedClient.FirstName;
        client.LastName = modifiedClient.LastName;
        client.Email = modifiedClient.Email;
        client.PhoneNumber = modifiedClient.PhoneNumber;
        client.DateOfBirth = modifiedClient.DateOfBirth;
        client.StreetAddress = modifiedClient.StreetAddress; 
        client.City = modifiedClient.City;
    }

    public static ClientDetailsResponse ToClientDetailsResponse(this Client client)
    {

        return new ClientDetailsResponse
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            DateOfBirth = client.DateOfBirth,
            Street = client.StreetAddress,
            City = client.City,
            ClientMembership = client.ClientMemberships
                                        .FirstOrDefault(item => item.IsActive)?.ToClientMembershipShortResponse(),
            IsActive = client.ClientMemberships.Any(item => item.IsActive &&  item.MembershipStatus == MembershipStatusEnum.Active) && client.ClientMemberships != null,
        };
    }


}
