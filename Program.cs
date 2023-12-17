using KAHA.TravelBot.NETCoreReactApp.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITravelBotService, TravelBotService>();
builder.Services.AddHttpClient("RestCountries", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://restcountries.com/v3.1/");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
});
builder.Services.AddHttpClient("SunriseSunset", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.sunrise-sunset.org/");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();