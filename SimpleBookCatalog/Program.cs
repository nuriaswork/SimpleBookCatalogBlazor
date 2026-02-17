using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Application.Interfaces;
using SimpleBookCatalog.Components;
using SimpleBookCatalog.Infraestructure.Context;
using SimpleBookCatalog.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Enable service provider validation in Development to catch DI lifetime issues early
//i.e., validate scopes (AddScoped, AddTransient, AddSingleton, etc.)
//ONLY IN NON-PROD as it adds overhead
builder.Host.UseDefaultServiceProvider((context, options) =>
{
    var isDev = context.HostingEnvironment.IsDevelopment();
    options.ValidateScopes = isDev;
    options.ValidateOnBuild = isDev;
});
/* With configuration toggle in appsettings.json:
// Read toggle from configuration 
var enableValidationFromConfig = builder.Configuration.GetValue<bool?>("ServiceProviderValidation:Enable");
var validateOnBuildFromConfig = builder.Configuration.GetValue<bool?>("ServiceProviderValidation:ValidateOnBuild");
// If the config key is not present, enable validation in Development by default
var enableValidation = enableValidationFromConfig ?? builder.Environment.IsDevelopment();
var validateOnBuild = validateOnBuildFromConfig ?? enableValidation;
// Enable service provider validation based on the configuration toggle:
builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = enableValidation;
    options.ValidateOnBuild = validateOnBuild;
});
 */

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//TODO: use a desing pattern to create the repository for different databases (e.g. MySQL, SQL Server, etc.)
builder.Services.AddDbContextFactory<SimpleBookCatalogDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQLConnection")!);
});

builder.Services.AddScoped<IBookRepository,BookRepository>();

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
