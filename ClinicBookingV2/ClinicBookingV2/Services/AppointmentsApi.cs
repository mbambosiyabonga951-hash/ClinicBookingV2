using System.Net.Http.Json;
using ClinicBooking.Client.Models;

namespace ClinicBooking.Client.Services;

public interface IAppointmentsApi
{
    Task<IReadOnlyList<AppointmentDto>> GetByClinicAndDateAsync(long clinicId, string yyyyMmDd, CancellationToken ct = default);
    Task<AppointmentDto?> CreateAsync(CreateAppointmentRequest dto, CancellationToken ct = default);
}

public sealed class AppointmentsApi : IAppointmentsApi
{
    private readonly IHttpClientFactory _factory;
    public AppointmentsApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<AppointmentDto>> GetByClinicAndDateAsync(long clinicId, string yyyyMmDd, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        var url = $"api/Appointments/by-clinic/{clinicId}/date/{yyyyMmDd}";
        var data = await c.GetFromJsonAsync<List<AppointmentDto>>(url, ct);
        return data ?? [];
    }

    public async Task<AppointmentDto?> CreateAsync(CreateAppointmentRequest dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/b", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<AppointmentDto>(cancellationToken: ct);
    }
}
