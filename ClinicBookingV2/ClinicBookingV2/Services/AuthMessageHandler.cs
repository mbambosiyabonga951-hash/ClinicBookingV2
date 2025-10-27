using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace ClinicBooking.Client.Services;

public sealed class AuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _storage;
    public AuthMessageHandler(ILocalStorageService storage) => _storage = storage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var token = await _storage.GetItemAsStringAsync("auth_token");
        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, ct);
    }
}
