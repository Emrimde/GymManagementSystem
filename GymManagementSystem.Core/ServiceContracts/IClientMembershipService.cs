using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.ClientMembership;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IClientMembershipService
{
    Task<Result<ClientMembershipInfoResponse>> CreateAsync(ClientMembershipAddRequest entity);
    Task<PageResult<ClientMembershipResponse>> GetAllAsync(string? searchText, int pageSize = 50, int page = 1);
    Task<Result<IEnumerable<ClientMembershipResponse>>> GetAllMembershipsClientHistoryAsync(Guid id);
    Task<Result<ClientMembershipDetailsResponse>> GetByIdAsync(Guid id);
    Task<Result<ClientMembershipContractPreviewResponse>> GetContractPreviewDetailsAsync(Guid clientId, Guid memebershipId);
    Task<Result<ClientMembershipWebResponse?>> GetClientMembershipInfoAsync();
    Task<Result<ClientMembershipWebPreviewResponse>> GetClientMembershipPreviewAsync(Guid membershipId);
}