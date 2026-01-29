using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class MembershipHttpClient : BaseHttpClientService
{
    public MembershipHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<MembershipResponse>>> GetAllMembershipsAsync()
    {
        return GetAsync<ObservableCollection<MembershipResponse>>("");
    }

    public Task<Result<MembershipResponse>> GetMembershipByIdAsync(Guid membershipId)
    {
        return GetAsync<MembershipResponse>($"{membershipId}");
    }

    public Task<Result<MembershipInfoResponse>> GetMembershipNameAsync(Guid membershipId)
    {
        return GetAsync<MembershipInfoResponse>(
            $"membership-name/{membershipId}"
        );
    }

    public Task<Result<MembershipResponse>> PostMembershipAsync(
        MembershipAddRequest request)
    {
        return PostAsync<MembershipAddRequest, MembershipResponse>(
            "",
            request
        );
    }

    public Task<Result<Unit>> PutMembershipAsync(
        MembershipUpdateRequest request,
        Guid membershipId)
    {
        return PutAsync<MembershipUpdateRequest, Unit>(
            $"{membershipId}",
            request
        );
    }

    // FEATURES

    public Task<Result<MembershipFeatureForEditResponse>> GetMembershipFeatureForEdit(
        Guid membershipFeatureId)
    {
        return GetAsync<MembershipFeatureForEditResponse>(
            $"get-membership-feature-for-edit/{membershipFeatureId}"
        );
    }

    public Task<Result<ObservableCollection<MembershipFeatureResponse>>> GetAllMembershipFeaturesByMembershipIdAsync(
        Guid membershipId)
    {
        return GetAsync<ObservableCollection<MembershipFeatureResponse>>(
            $"get-membership-features/{membershipId}"
        );
    }

    public Task<Result<Unit>> PostMembershipFeatureAsync(
        MembershipFeatureAddRequest request)
    {
        return PostAsync<MembershipFeatureAddRequest, Unit>(
            "create-membership-feature",
            request
        );
    }

    public Task<Result<Unit>> UpdateMembershipFeature(
        MembershipFeatureUpdateRequest request)
    {
        return PutAsync<MembershipFeatureUpdateRequest, Unit>(
            "update-membership-feature",
            request
        );
    }

    public Task<Result<Unit>> DeleteMembershipFeatureAsync(
        Guid membershipFeatureId)
    {
        return DeleteAsync(
            $"delete-membership-feature/{membershipFeatureId}"
        );
    }
}
