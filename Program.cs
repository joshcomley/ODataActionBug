using Microsoft.AspNetCore.OData;
using ODataActionBug;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorPages();

builder.Services
    .AddControllersWithViews()
    .AddOData(
        opt => opt.AddRouteComponents("v1", ODataConfig.GetEdmModel()).Filter().Select().Expand()
    );

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.UseODataRouteDebug();

app.Run();