using ClinicBooking.Client.Models;

public interface ITimeslotsApi
{
    Task<IReadOnlyList<TimeslotDto>> GetAsync(CancellationToken ct = default);
    Task<TimeslotDto?> CreateAsync(CreateTimeslotRequest dto, CancellationToken ct = default);
}

public sealed class TimeslotsApi : ITimeslotsApi
{
    private readonly IHttpClientFactory _factory;
    public TimeslotsApi(IHttpClientFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<TimeslotDto>> GetAsync(CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        var data = await c.GetFromJsonAsync<List<TimeslotDto>>("api/Timeslots", ct);
        return data ?? [];
    }

    public async Task<TimeslotDto?> CreateAsync(CreateTimeslotRequest dto, CancellationToken ct = default)
    {
        var c = _factory.CreateClient("Api");
        using var resp = await c.PostAsJsonAsync("api/Timeslots", dto, ct);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<TimeslotDto>(cancellationToken: ct);
    }
}