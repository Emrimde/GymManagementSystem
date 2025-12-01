using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


namespace GymManagementSystem.WPF.HttpServices;
public class EmployeeHttpClient : BaseHttpClientService
{
    public EmployeeHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<EmployeeInfoResponse>> PostEmployeeAsync(EmployeeAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            EmployeeInfoResponse? employee = JsonSerializer.Deserialize<EmployeeInfoResponse>(responseBody);
            if (employee == null)
            {
                return Result<EmployeeInfoResponse>.Failure("Unexpected employee");
            }
            return Result<EmployeeInfoResponse>.Success(employee);
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<EmployeeInfoResponse>.Failure(errorMessage);
        }
        if(errorDict != null &&  errorDict.TryGetValue("errors", out var errorsElement))
        {
            List<string> allErrors = new List<string>();
            foreach (var errorProp in errorsElement.EnumerateObject())
            {
                var messages = errorProp.Value.EnumerateArray();
                foreach (var item in messages)
                {
                    string msg = item.ToString();
                    allErrors.Add(msg);
                }
            }
            return Result<EmployeeInfoResponse>.Failure(string.Join("\n", allErrors));
        }

        return Result<EmployeeInfoResponse>.Failure("Something went wrong.");
    }

    public async Task<Result<ObservableCollection<EmployeeResponse>>> GetAllEmployeesAsync()
    {
        try
        {
            ObservableCollection<EmployeeResponse>? response = await _httpClient.GetFromJsonAsync<ObservableCollection<EmployeeResponse>>("");
            return Result<ObservableCollection<EmployeeResponse>>.Success(response ?? new ObservableCollection<EmployeeResponse>());
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<EmployeeResponse>>.Failure(ex.Message);
        }
    }
}
