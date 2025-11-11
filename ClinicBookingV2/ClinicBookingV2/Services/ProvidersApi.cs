using ClinicBooking.Client.Models;

public interface IProvidersApi
{
    Task<IReadOnlyList<ProviderDto>> GetAsync(CancellationToken ct = default);
    Task<ProviderDto?> CreateAsync(UpsertProviderRequest dto, CancellationToken ct = default);
}

public sealed class ProvidersApi : IProvidersApi
{
    private readonly IHttpClientFactory _factory;
    public ProvidersApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<ProviderDto>> GetAsync(CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        var data = await c.GetFromJsonAsync<List<ProviderDto>>("api/Providers", ct);
        return data ?? [];
    }

    public async Task<ProviderDto?> CreateAsync(UpsertProviderRequest dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/Providers", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<ProviderDto>(cancellationToken: ct);
    }
}