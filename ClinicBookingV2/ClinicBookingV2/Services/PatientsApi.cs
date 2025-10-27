using System.Net.Http.Json;
using ClinicBooking.Client.Models;

namespace ClinicBooking.Client.Services;

public interface IPatientsApi
{
    Task<IReadOnlyList<PatientDto>> GetAsync(CancellationToken ct = default);
    Task<PatientDto?> CreateAsync(CreatePatientRequest dto, CancellationToken ct = default);
}

public sealed class PatientsApi : IPatientsApi
{
    private readonly IHttpClientFactory _factory;
    public PatientsApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<PatientDto>> GetAsync(CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        var data = await c.GetFromJsonAsync<List<PatientDto>>("api/Patients", ct);
        return data ?? [];
    }

    public async Task<PatientDto?> CreateAsync(CreatePatientRequest dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/Patients", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<PatientDto>(cancellationToken: ct);
    }
}
