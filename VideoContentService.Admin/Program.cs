using VideoContentService.Admin.Services;
using VideoContentService.Admin.Properties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Refresh razor pages on change
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()

// Add configuration for the API and Admin
builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("API"));
builder.Services.Configure<AdminConfig>(builder.Configuration.GetSection("AdminCredentials"));

// Add Services for the API
builder.Services.AddHttpClient<VideoService>();
builder.Services.AddHttpClient<CountryService>();
builder.Services.AddHttpClient<TagService>();
builder.Services.AddHttpClient<GenreService>();
builder.Services.AddHttpClient<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Admin/Index");
//}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
