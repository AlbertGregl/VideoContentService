using VideoContentService.Public.Services;
using VideoContentService.Public.Properties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Refresh razor pages on change
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()

// Add configuration for the API
builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("API"));

// Add Services for the API
builder.Services.AddHttpClient<PublicUserService>();
builder.Services.AddHttpClient<VideoUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/PublicUser/Error");
//}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PublicUser}/{action=Index}/{id?}");

app.Run();
