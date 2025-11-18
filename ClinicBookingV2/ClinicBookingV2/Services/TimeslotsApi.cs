using ClinicBooking.Client.Models;
using ClinicBookingV2.Client.Components.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

public interface ITimeslotsApi
{
    Task<IReadOnlyList<TimeslotDto>> GetByProviderAndDateAsync(long providerId, DateOnly date, CancellationToken ct = default);
    Task<TimeslotDto?> CreateAsync(CreateTimeslotRequest dto, CancellationToken ct = default);
}

public sealed class TimeslotsApi : ITimeslotsApi
{
    private readonly IHttpClientFactory _factory;
    public TimeslotsApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<TimeslotDto>> GetByProviderAndDateAsync(long providerId, DateOnly date, CancellationToken ct = default)
    {
        try {
            var c = _factory.CreateClient("Api");
            //https://localhost:51657/api/Timeslots/by-provider/1/date/2025-10-11
            var url = $"api/Timeslots/by-provider/{providerId}/date/{date:yyyy-MM-dd}";
            var data = await c.GetFromJsonAsync<List<TimeslotDto>>(url, ct);
            return data ?? [];
        }
        catch (Exception ex)
        {

            return [];
        }

    }

    public async Task<TimeslotDto?> CreateAsync(CreateTimeslotRequest dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/Timeslots", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<TimeslotDto>(cancellationToken: ct);
    }
}