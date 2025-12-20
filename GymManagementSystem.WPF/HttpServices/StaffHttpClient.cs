using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

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

}
