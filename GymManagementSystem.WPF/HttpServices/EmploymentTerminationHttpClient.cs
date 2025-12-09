using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class EmploymentTerminationHttpClient : BaseHttpClientService
{
    public EmploymentTerminationHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(Guid personId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<EmploymentTerminationGenerateResponse>($"{personId}");
            return Result<EmploymentTerminationGenerateResponse>.Success(response);
        }
        catch (HttpRequestException ex)
        {
            return Result<EmploymentTerminationGenerateResponse>.Failure($"Error fetching employment termination details: {ex.Message}");
        }
    }

    public async Task<Result<EmploymentTerminationInfoResponse>> CreateEmploymentTerminationAsync(EmploymentTerminationAddRequest request)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                EmploymentTerminationInfoResponse? employmentTermination = JsonSerializer.Deserialize<EmploymentTerminationInfoResponse>(responseBody);
                if (employmentTermination == null)
                {
                    return Result<EmploymentTerminationInfoResponse>.Failure("Unexpected employee");
                }
                return Result<EmploymentTerminationInfoResponse>.Success(employmentTermination);
            }
            else
            {
                Dictionary<string, JsonElement> errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody)!;
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    string detailMessage = detailElement.GetString() ?? responseBody;
                    return Result<EmploymentTerminationInfoResponse>.Failure(detailMessage);
                }
                if (errorDict != null && errorDict.TryGetValue("errors", out var errorsElement))
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
                    return Result<EmploymentTerminationInfoResponse>.Failure(string.Join("\n", allErrors));
                }

                return Result<EmploymentTerminationInfoResponse>.Failure("Unexpected error");
            }
        }
        catch (HttpRequestException ex)
        {
            return Result<EmploymentTerminationInfoResponse>.Failure($"Error creating employment termination: {ex.Message}");
        }
    }

    public async Task<Result<ObservableCollection<EmploymentTerminationResponse>>> GetAllEmploymentTerminationsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ObservableCollection<EmploymentTerminationResponse>>("");
            return Result<ObservableCollection<EmploymentTerminationResponse>>.Success(response);
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<EmploymentTerminationResponse>>.Failure($"Error fetching employment terminations: {ex.Message}");
        }
    }
}
