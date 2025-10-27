using System.Net.Http.Json;
using ClinicBooking.Client.Models;

namespace ClinicBooking.Client.Services;

public interface IAuthApi
{
    Task<string?> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
    Task<string?> LoginAsync(LoginDto dto, CancellationToken ct = default);
}

public sealed class AuthApi : IAuthApi
{
    private readonly IHttpClientFactory _factory;
    public AuthApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<string?> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/auth/register", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        var body = await resp.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken: ct);
        return body?.token;
    }

    public async Task<string?> LoginAsync(LoginDto dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/auth/login", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        var body = await resp.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken: ct);
        return body?.token;
    }
}
