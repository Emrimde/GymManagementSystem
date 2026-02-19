using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.ClientMembership;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.Core.Services;

public class ClientMembershipService : IClientMembershipService
{
    private readonly IClientMembershipRepository _clientMembershipRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IRepository<ContractResponse, Contract> _contractRepo;
    private readonly IMembershipRepository _membershipRepo;
    private readonly IMembershipPriceRepository _membershipPriceRepo;
    private readonly IUnitOfWork _unitOfWork;


    private readonly IHttpContextAccessor _contextAccessor;


    public ClientMembershipService(IClientMembershipRepository clientMembershipRepository, IRepository<ContractResponse, Contract> contractRepo, IMembershipRepository membershipRepo, IClientRepository clientRepository, IMembershipPriceRepository membershipPriceRepo, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
    {
        _clientMembershipRepository = clientMembershipRepository;
        _contractRepo = contractRepo;
        _membershipRepo = membershipRepo;
        _clientRepository = clientRepository;
        _membershipPriceRepo = membershipPriceRepo;
        _contextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<ClientMembershipInfoResponse>> CreateAsync(ClientMembershipAddRequest entity)
    {
        ClientMembership clientMembership = entity.ToClientMembership();
        Client? client = new();
        if (entity.IsFromWeb)
        {
            string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
            if (!Guid.TryParse(claim, out var clientId))
            {
                return Result<ClientMembershipInfoResponse>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
            }
            clientMembership.ClientId = clientId;
            client = await _clientRepository.GetByIdAsync(clientId);
        }
        else
        {
            client = await _clientRepository.GetByIdAsync(entity.ClientId);
        }

        if(client == null)
        {
            return Result<ClientMembershipInfoResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        DateTime minimalYear = DateTime.UtcNow.AddYears(-13);
        if (client.DateOfBirth >= minimalYear)
        {
            return Result<ClientMembershipInfoResponse>.Failure(
                "You must be at least 13 years old to purchase a membership.",
                StatusCodeEnum.BadRequest);
        }

        ClientMembership? activeMembership = await _clientMembershipRepository.GetActiveClientMembershipByClientId(client.Id);

        if (activeMembership != null)
        {
            return Result<ClientMembershipInfoResponse>.Failure("You can't add membership for this client - client has membership", StatusCodeEnum.BadRequest);
        }

        Membership? membership = await _membershipRepo.GetByIdAsync(entity.MembershipId);
        if (membership == null)
        {
            return Result<ClientMembershipInfoResponse>.Failure("Membership not found",StatusCodeEnum.NotFound);
        }



        if (membership.MembershipType == MembershipTypeEnum.Annual)
        {
            clientMembership.EndDate = clientMembership.StartDate.AddYears(1);
        }

        else if (membership.MembershipType == MembershipTypeEnum.Monthly)
        {
            clientMembership.EndDate = null;
        }

        _clientMembershipRepository.CreateAsync(clientMembership);
        client.IsActive = true;
        Contract contract = new Contract()
        {
            ClientMembershipId = clientMembership.Id,
            CreatedAt = clientMembership.StartDate,
            ContractStatus = ContractStatus.Draft,
            IsActive = true
        };
        _contractRepo.CreateAsync(contract);
        await _unitOfWork.SaveChangesAsync();
        return Result<ClientMembershipInfoResponse>.Success(clientMembership.ToClientMembershipInfoResponse(contract.Id));
    }

    public async Task<PageResult<ClientMembershipResponse>> GetAllAsync(string? searchText, int pageSize = 50, int page = 1)
    {
        PageResult<ClientMembershipResponse> clientMembershipList = await _clientMembershipRepository.GetAllAsync(pageSize: pageSize, page: page, searchText: searchText);
        return clientMembershipList;
    }

    public async Task<Result<IEnumerable<ClientMembershipResponse>>> GetAllMembershipsClientHistoryAsync(Guid id)
    {
        IEnumerable<ClientMembershipResponse> clientMemberships = await _clientMembershipRepository.GetAllClientMemberships(id);
        return Result<IEnumerable<ClientMembershipResponse>>.Success(clientMemberships);
    }

    public async Task<Result<ClientMembershipDetailsResponse>> GetByIdAsync(Guid id)
    {
        ClientMembership? clientMembership = await _clientMembershipRepository.GetByIdAsync(id);
        if (clientMembership == null)
        {
            return Result<ClientMembershipDetailsResponse>.Failure("Cannot open client membership details");
        }
        return Result<ClientMembershipDetailsResponse>.Success(clientMembership.ToClientMembershipDetailsResponse());
    }

    public async Task<Result<ClientMembershipWebResponse?>> GetClientMembershipInfoAsync()
    {
        string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<ClientMembershipWebResponse?>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        ClientMembershipWebResponse? dto = await _clientMembershipRepository.GetClientMembershipByClientIdAsync(clientId);

        return Result<ClientMembershipWebResponse?>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientMembershipWebPreviewResponse>> GetClientMembershipPreviewAsync(Guid membershipId)
    {
        string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<ClientMembershipWebPreviewResponse>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        MembershipPrice? membershipPrice = await _membershipPriceRepo.GetActiveMembershipPriceByMembershipId(membershipId);
        if (membershipPrice == null)
        {
            return Result<ClientMembershipWebPreviewResponse>.Failure("No price found", StatusCodeEnum.InternalServerError);
        }

        MembershipInfoResponse? membershipInfo = await _membershipRepo.GetMembershipNameAsync(membershipId);
        if (membershipInfo == null)
        {
            return Result<ClientMembershipWebPreviewResponse>.Failure("No name found", StatusCodeEnum.InternalServerError);
        }

        ClientDetailsWebResponse? client = await _clientRepository.GetClientProfileInfoAsync(clientId);
        if (client == null)
        {
            return Result<ClientMembershipWebPreviewResponse>.Failure("No client details found", StatusCodeEnum.InternalServerError);
        }
        ClientMembershipWebPreviewResponse response = new ClientMembershipWebPreviewResponse()
        {
            City = client.City,
            DateOfBirth = client.DateOfBirth.HasValue && client.DateOfBirth.Value.Date == DateTime.MinValue.Date ? null : client.DateOfBirth,
            Email = client.Email,
            FirstName = client.FirstName,
            LastName = client.LastName,
            MembershipName = membershipInfo.MembershipName,
            Price = membershipPrice.Price.ToString(),
            PhoneNumber = client.PhoneNumber,
            Street = client.Street,
        };

        return Result<ClientMembershipWebPreviewResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientMembershipContractPreviewResponse>> GetContractPreviewDetailsAsync(Guid clientId, Guid membershipId)
    {
        Client? client = await _clientRepository.GetByIdAsync(clientId);
        Membership? membership = await _membershipRepo.GetByIdAsync(membershipId);
        MembershipPrice? actualPrice = await _membershipPriceRepo.GetActiveMembershipPriceByMembershipId(membershipId);
        if (actualPrice == null)
        {
            return Result<ClientMembershipContractPreviewResponse>.Failure("Error during loading actual price", StatusCodeEnum.InternalServerError);
        }

        if (membership == null || client == null)
        {
            return Result<ClientMembershipContractPreviewResponse>.Failure("Client or membership not found", StatusCodeEnum.NotFound);
        }
        string? endDate = membership.MembershipType == MembershipTypeEnum.Monthly ? null : DateTime.UtcNow.AddYears(1).ToString("dd.MM.yyyy");

        ClientMembershipContractPreviewResponse response = new ClientMembershipContractPreviewResponse()
        {
            StartDate = DateTime.UtcNow.ToString("dd.MM.yyyy"),
            EndDate = endDate,
            FullName = client.FirstName + " " + client.LastName,
            MembershipName = membership.Name,
            Price = actualPrice.Price.ToString(),
        };

        return Result<ClientMembershipContractPreviewResponse>.Success(response);
    }

}
