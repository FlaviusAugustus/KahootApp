using DefaultNamespace;
using KahootFrontend.Components;
using KahootFrontend.Services;
using KahootFrontend.Services.KahootAuthStateProvider;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ApiService>();

builder.Services.AddScoped<AuthenticationStateProvider, KahootAuthStateProvider>();

builder.Services.AddCascadingAuthenticationState();

builder.Services.Configure<HttpClientOptions>(builder.Configuration.GetSection(HttpClientOptions.Section));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
