using GymManagementSystem.WPF.Services;
using System.Net.Http;
using System.Net.Http.Headers;

public class JwtHandler : DelegatingHandler
{
    private readonly AuthService _auth;

    public JwtHandler(AuthService auth) => _auth = auth;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(_auth.JwtToken))
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _auth.JwtToken);

        return await base.SendAsync(request, ct);
    }
}
