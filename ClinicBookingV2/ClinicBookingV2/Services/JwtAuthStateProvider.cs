using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClinicBooking.Client.Services;

public sealed class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _storage;
    private static readonly AuthenticationState _anon =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    public JwtAuthStateProvider(ILocalStorageService storage) => _storage = storage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storage.GetItemAsStringAsync("auth_token");
        if (string.IsNullOrWhiteSpace(token)) return _anon;

        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token)) return _anon;

        var jwt = handler.ReadJwtToken(token);
        if (jwt.ValidTo < DateTime.UtcNow) { await LogoutAsync(); return _anon; }

        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task LoginAsync(string token)
    {
        await _storage.SetItemAsStringAsync("auth_token", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveItemAsync("auth_token");
        NotifyAuthenticationStateChanged(Task.FromResult(_anon));
    }
}
