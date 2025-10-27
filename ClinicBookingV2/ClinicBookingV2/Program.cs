//using BlazorApp2.Client.Pages;
using ClinicBooking.Client.Services;
using ClinicBookingV2.Client.Components;
using Microsoft.FluentUI.AspNetCore.Components;

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
// App services
//builder.Services.AddScoped<IAuthApi, AuthApi>();
builder.Services.AddScoped<IClinicsApi, ClinicsApi>();
builder.Services.AddScoped<IPatientsApi, PatientsApi>();
builder.Services.AddScoped<IAppointmentsApi, AppointmentsApi>();

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
    .AddInteractiveWebAssemblyRenderMode();
    //.AddAdditionalAssemblies(typeof(BlazorApp2.Client._Imports).Assembly);

app.Run();
