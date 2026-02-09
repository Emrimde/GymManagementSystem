using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.Services;
using GymManagementSystem.WPF.ViewModels.Auth;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

public class JwtHandler : DelegatingHandler
{
    private readonly AuthService _auth;
    private readonly INavigationService _navigation;

    public JwtHandler(AuthService auth, INavigationService navigation)
    {
        _auth = auth;
        _navigation = navigation;
    }


    protected override async Task<HttpResponseMessage> SendAsync(
          HttpRequestMessage request, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(_auth.JwtToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.JwtToken);
        }

        var response = await base.SendAsync(request, ct);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _auth.ClearJwt();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _navigation.NavigateTo<LoginViewModel>();
            });
        }

        return response;
    }
}
