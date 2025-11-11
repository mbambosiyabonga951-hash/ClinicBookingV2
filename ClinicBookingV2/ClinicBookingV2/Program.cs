//using BlazorApp2.Client.Pages;
using ClinicBooking.Client.Services;
using ClinicBookingV2.Client.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddHttpClient(); // registers IHttpClientFactory
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:51657/");
    // client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// register Blazored.LocalStorage so ILocalStorageService is available
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


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddAuthorization();
// optionally configure policies
builder.Services.AddAuthorization(options =>
{
    // options.AddPolicy("MyPolicy", policy => policy.RequireClaim("..."));
});

builder.Services.AddFluentUIComponents();

// Program.cs (Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()        // needed for InteractiveServer
    .AddInteractiveWebAssemblyComponents();  // if you use InteractiveWebAssembly/Auto
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode();


//var app = builder.Build();
//app.MapRazorComponents<App>()
//   .()
//   .AddInteractiveWebAssemblyRenderMode();


app.Run();
