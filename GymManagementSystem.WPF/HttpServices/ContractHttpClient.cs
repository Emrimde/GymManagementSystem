using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ContractHttpClient : BaseHttpClientService
{
    public ContractHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }


   
}
