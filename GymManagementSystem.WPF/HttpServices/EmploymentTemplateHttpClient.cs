using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class EmploymentTemplateHttpClient : BaseHttpClientService
{
    public EmploymentTemplateHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<EmploymentTemplateInfoResponse>> PostEmploymentTemplateAsync(EmploymentTemplateAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            EmploymentTemplateInfoResponse? employee = JsonSerializer.Deserialize<EmploymentTemplateInfoResponse>(responseBody);
            if (employee == null)
            {
                return Result<EmploymentTemplateInfoResponse>.Failure("Unexpected employee");
            }
            return Result<EmploymentTemplateInfoResponse>.Success(employee);
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<EmploymentTemplateInfoResponse>.Failure(errorMessage);
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
            return Result<EmploymentTemplateInfoResponse>.Failure(string.Join("\n", allErrors));
        }

        return Result<EmploymentTemplateInfoResponse>.Failure("Something went wrong.");
    }

    public async Task<Result<ObservableCollection<EmploymentTemplateResponse>>> GetAllEmploymentTemplates()
    {
        try
        {
            ObservableCollection<EmploymentTemplateResponse>? response = await   _httpClient.GetFromJsonAsync<ObservableCollection<EmploymentTemplateResponse>>("");
            return Result<ObservableCollection<EmploymentTemplateResponse>>.Success(response ?? new ObservableCollection<EmploymentTemplateResponse>());
        }
        catch(HttpRequestException ex)
        {
            return Result<ObservableCollection<EmploymentTemplateResponse>>.Failure($"Unexpected error: {ex.Message}");
        }
    }
}
