using System.Threading.Tasks;
using VideoContentService.Admin.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Refresh razor pages on change
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()

// Add the service for the admin API
builder.Services.AddScoped<IAdminApiService, AdminApiService>();
// Add the service for the IMapper
builder.Services.AddAutoMapper(
    typeof(IntegrationModule.Mapping.AutomapperProfile),
    typeof(VideoContentService.Admin.Mapping.AdminMappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Admin/ManageVideos");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=ManageVideos}/{id?}");

app.Run();
