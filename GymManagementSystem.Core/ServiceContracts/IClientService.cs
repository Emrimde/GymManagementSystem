using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.Client;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IClientService
{
    Task<Result<Unit>> CreateAccountAsync(ClientWebAddRequest entity);
    Task<Result<ClientInfoResponse>> CreateAsync(ClientAddRequest entity);
    Task<PageResult<ClientResponse>> GetAllAsync(string? searchText, int page);
    Task<Result<ClientDetailsResponse>> GetByIdAsync(Guid id);
    Task<Result<ClientDetailsWebResponse>> GetClientDetailsByUserIdAsync();
    Task<Result<ClientInfoResponse>> GetClientFullNameByIdAsync(Guid id);
    Task<Result<IEnumerable<ClientInfoResponse>>> LookUpClientsAsync(string query, Guid? scheduledClassId = null);
    Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest entity);
    Result<ClientAgeValidationResponse> ValidateClientAgeAsync(ClientAgeValidationRequest entity);
}
