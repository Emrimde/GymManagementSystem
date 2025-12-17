using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;
using System.Net.Http;

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
}
