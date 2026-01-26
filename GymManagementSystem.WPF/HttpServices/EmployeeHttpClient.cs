using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class EmployeeHttpClient : BaseHttpClientService
{
    public EmployeeHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<EmploymentContractPdfDto>> GetEmployeeContractAsync(
        EmployeeContractRequest request)
    {
        return PostAsync<EmployeeContractRequest, EmploymentContractPdfDto>(
            "get-employee-contract",
            request
        );
    }

    public Task<Result<EmployeeInfoResponse>> PostEmployeeAsync(
        EmployeeAddRequest request)
    {
        return PostAsync<EmployeeAddRequest, EmployeeInfoResponse>(
            "",
            request
        );
    }

    public Task<Result<ObservableCollection<EmployeeResponse>>> GetAllEmployeesAsync(
        string? searchText = null)
    {
        string query = string.IsNullOrWhiteSpace(searchText)
            ? ""
            : $"?searchText={Uri.EscapeDataString(searchText)}";

        return GetAsync<ObservableCollection<EmployeeResponse>>(query);
    }

    public Task<Result<EmployeeDetailsResponse>> GetEmployeeByIdAsync(
        Guid employeeId)
    {
        return GetAsync<EmployeeDetailsResponse>(
            $"{employeeId}"
        );
    }
}
