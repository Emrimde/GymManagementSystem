using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class VisitHttpClient : BaseHttpClientService
{
    public VisitHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<Unit>> RegisterVisitAsync(Guid clientId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync($"register-visit/{clientId}", null);
            if(response.IsSuccessStatusCode)
            {
                return Result<Unit>.Success(new Unit());
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return Result<Unit>.Failure(errorMessage);
            }
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Error registering visit: {ex.Message}");
        }
    }


    public async Task<Result<ObservableCollection<VisitResponse>>> GetAllClientVisitsAsync(Guid clientId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ObservableCollection<VisitResponse>>($"{clientId}");

            return Result<ObservableCollection<VisitResponse>>.Success(response ?? new ObservableCollection<VisitResponse>());
        }
            
        
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<VisitResponse>>.Failure($"Error fetching visits: {ex.Message}");
        }
    }
}
