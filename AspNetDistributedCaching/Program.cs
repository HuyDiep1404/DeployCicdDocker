using AspNetDistributedCaching.Core.Options;
using AspNetDistributedCaching.Core.Repositories;
using AspNetDistributedCaching.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
IConfiguration? Configuration = builder.Configuration;
AppConfig cfg = Configuration.Get<AppConfig>();
// Add services to the container.


builder.Services.AddSingleton(cfg.Mongo);
builder.Services.AddTransient<ICatalogRepository, CatalogRepository>();
builder.Services.AddTransient<ICatalogSvc, CatalogSvc>();

builder.Services.AddStackExchangeRedisCache(o =>
{

    o.Configuration = cfg.Redis.Configuration;
    o.InstanceName = cfg.Redis.InstanceName;
});
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
