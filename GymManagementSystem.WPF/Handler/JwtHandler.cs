using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.Services;
using GymManagementSystem.WPF.ViewModels.Auth;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

public class JwtHandler : DelegatingHandler
{
    private readonly IServiceProvider _provider;
    private readonly INavigationService _navigation;

    public JwtHandler(IServiceProvider provider,
                      INavigationService navigation)
    {
        _provider = provider;
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        var auth = _provider.GetRequiredService<AuthService>();

        if (!string.IsNullOrEmpty(auth.JwtToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", auth.JwtToken);
        }

        var response = await base.SendAsync(request, ct);

        if (response.StatusCode == HttpStatusCode.Unauthorized &&
            !request.RequestUri!.AbsolutePath.Contains("refresh"))
        {
            var refreshSuccess = await auth.RefreshAsync();

            if (!refreshSuccess)
            {
                auth.ClearJwt();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _navigation.NavigateTo<LoginViewModel>();
                });

                return response;
            }

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", auth.JwtToken);

            return await base.SendAsync(request, ct);
        }

        return response;
    }
}
