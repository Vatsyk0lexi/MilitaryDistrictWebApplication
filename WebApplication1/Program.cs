using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MilitaryDistrictContext>(opt =>
{
    opt.UseSqlServer("data source=PALMA;initial catalog=MilitaryDistrict;trusted_connection=true;Encrypt=False");
});


builder.Services.AddScoped<IMilitaryRepository, MilitaryRepository>();
builder.Services.AddScoped<IMilitaryBaseRepository, MilitaryBaseRepository>();

builder.Services.AddScoped<IMilitaryService,MilitaryService >();
builder.Services.AddScoped<IMilitaryBaseService, MilitaryBaseService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
