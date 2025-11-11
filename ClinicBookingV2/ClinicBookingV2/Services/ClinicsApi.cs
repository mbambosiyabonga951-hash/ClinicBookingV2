using System.Net.Http.Json;
using ClinicBooking.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicBooking.Client.Services;

public interface IClinicsApi
{
    Task<IReadOnlyList<ClinicDto>> GetAsync(CancellationToken ct = default);
}

public sealed class ClinicsApi : IClinicsApi
{
    private readonly IHttpClientFactory _factory;
    public ClinicsApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<ClinicDto>> GetAsync(CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        var data = await c.GetFromJsonAsync<List<ClinicDto>>("api/Clinics", ct);
        return data ?? [];
    }
}
