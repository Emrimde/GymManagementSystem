using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public abstract class BaseHttpClientService
{
    protected readonly HttpClient _httpClient;    
    protected BaseHttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
