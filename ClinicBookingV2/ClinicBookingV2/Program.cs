using ClinicBooking.Client.Services;
using ClinicBookingV2.Client.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + interactivity
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()        // InteractiveServer
    .AddInteractiveWebAssemblyComponents();  // InteractiveWebAssembly / Auto

builder.Services.AddFluentUIComponents();

// SignalR (if you actually use it)
builder.Services.AddSignalR();

// HTTP clients
builder.Services.AddHttpClient(); // IHttpClientFactory

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiBaseUrl"] ?? "https://localhost:51657/");
});

// Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// App services
builder.Services.AddHttpClient<ClinicsApi>();
builder.Services.AddHttpClient<PatientsApi>();
builder.Services.AddHttpClient<ProvidersApi>();
builder.Services.AddHttpClient<AppointmentsApi>();

builder.Services.AddScoped<IAuthApi, AuthApi>();
builder.Services.AddScoped<IClinicsApi, ClinicsApi>();
builder.Services.AddScoped<IPatientsApi, PatientsApi>();
builder.Services.AddScoped<IProvidersApi, ProvidersApi>();
builder.Services.AddScoped<IAppointmentsApi, AppointmentsApi>();
builder.Services.AddScoped<ITimeslotsApi, TimeslotsApi>();

// Auth & authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

builder.Services.AddAuthorization(options =>
{
    // options.AddPolicy("MyPolicy", policy => policy.RequireClaim("..."));
});

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    // Needed when using Interactive WebAssembly
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// This is the ONLY endpoint mapping you need for a Blazor Web App
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode();

app.Run();
