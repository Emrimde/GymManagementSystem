using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class StaffHttpClient : BaseHttpClientService
{
    public StaffHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<ObservableCollection<PersonResponse>>> GetAllStaffAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ObservableCollection<PersonResponse>>($"");

            return Result<ObservableCollection<PersonResponse>>.Success(response ?? new ObservableCollection<PersonResponse>());
        }


        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<PersonResponse>>.Failure($"Error fetching staff: {ex.Message}");
        }
    }

    public async Task<Result<PersonInfoResponse>> PostPersonToStaffAsync(PersonAddRequest request)
    {

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            PersonInfoResponse? person = JsonSerializer.Deserialize<PersonInfoResponse>(responseBody);
            if (person == null)
            {
                return Result<PersonInfoResponse>.Failure("Unexpected person add error");
            }
            return Result<PersonInfoResponse>.Success(person);
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<PersonInfoResponse>.Failure(errorMessage);
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
            return Result<PersonInfoResponse>.Failure(string.Join("\n", allErrors));
        }

        return Result<PersonInfoResponse>.Failure("Something went wrong.");
    }

}
