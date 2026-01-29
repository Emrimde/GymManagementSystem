using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class TrainerHttpClient : BaseHttpClientService
{
    public TrainerHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    // TIME OFF

    public Task<Result<Unit>> PostTrainerTimeOff(
        TrainerTimeOffAddRequest request)
    {
        return PostAsync<TrainerTimeOffAddRequest, Unit>(
            "timeoff",
            request
        );
    }

    public Task<Result<TrainerTimeOff>> UpdateAsync(
        Guid id,
        TrainerTimeOffUpdateRequest dto)
    {
        return PutAsync<TrainerTimeOffUpdateRequest, TrainerTimeOff>(
            $"trainer-timeoff/{id}",
            dto
        );
    }

    public Task<Result<Unit>> DeleteAsync(Guid id)
    {
        return DeleteAsync($"{id}");
    }

    // SCHEDULE

    public Task<Result<TrainerScheduleResponse>> GetSchedule(
        Guid trainerId,
        int days = 30)
    {
        return GetAsync<TrainerScheduleResponse>(
            $"schedule/{trainerId}?days={days}"
        );
    }

    // CONTRACTS

    public Task<Result<PageResult<TrainerContractResponse>>> GetTrainerContracts(
        string? searchText,
        int page = 1)
    {
        string query = string.IsNullOrWhiteSpace(searchText)
            ? $"trainercontracts?page={page}"
            : $"trainercontracts?searchText={Uri.EscapeDataString(searchText)}&page={page}";

        return GetAsync<PageResult<TrainerContractResponse>>(query);
    }

    public Task<Result<TrainerContractInfoResponse>> PostTrainerContractAsync(
        TrainerContractAddRequest request)
    {
        return PostAsync<TrainerContractAddRequest, TrainerContractInfoResponse>(
            "trainercontract",
            request
        );
    }

    public Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(
        Guid trainerContractId,
        bool includeDetails)
    {
        return GetAsync<TrainerContractDetailsResponse>(
            $"trainercontract/{trainerContractId}?includeDetails={includeDetails}"
        );
    }

    // RATES

    public Task<Result<ObservableCollection<TrainerRateResponse>>> GetTrainerRatesAsync(
        Guid id)
    {
        return GetAsync<ObservableCollection<TrainerRateResponse>>(
            $"trainer-rates/{id}"
        );
    }

    public Task<Result<ObservableCollection<TrainerRateSelectResponse>>> GetTrainerRatesSelectAsync(
        Guid id)
    {
        return GetAsync<ObservableCollection<TrainerRateSelectResponse>>(
            $"trainer-rates-select/{id}"
        );
    }

    public Task<Result<TrainerRateInfoResponse>> AddTrainerRateAsync(
        TrainerRateAddRequest request)
    {
        request.ValidFrom = request.ValidFrom.ToUniversalTime();

        return PostAsync<TrainerRateAddRequest, TrainerRateInfoResponse>(
            "trainer-rate",
            request
        );
    }

    // TRAINERS

    public Task<Result<ObservableCollection<TrainerInfoResponse>>> GetPersonalTrainersAsync()
    {
        return GetAsync<ObservableCollection<TrainerInfoResponse>>(
            "personal-trainers"
        );
    }

    public Task<Result<ObservableCollection<TrainerContractInfoResponse>>> GetInstructors()
    {
        return GetAsync<ObservableCollection<TrainerContractInfoResponse>>(
            "instructors"
        );
    }
}
