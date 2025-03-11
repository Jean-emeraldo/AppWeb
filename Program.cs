using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GamingStore.Data;
using GamingStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services de contrôleurs et de vues
builder.Services.AddControllersWithViews();

// Ajouter la base de données en mémoire
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("GamingStoreDB"));

// Ajouter le filtre d'exception pour les pages de développement
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Activer les fichiers statiques (pour que le navigateur puisse accéder aux fichiers JS, CSS, etc.)
app.UseStaticFiles();

// Initialiser la base de données (si nécessaire)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

// Configurer les routes pour les contrôleurs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

// Démarrer l'application
app.Run();
