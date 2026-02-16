using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Application.Interfaces;
using SimpleBookCatalog.Components;
using SimpleBookCatalog.Infraestructure.Context;
using SimpleBookCatalog.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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
