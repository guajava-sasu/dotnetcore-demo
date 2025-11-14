using ModustaAPI;
using ModustaAPI.Models;
using ModustaAPI.Services;
using Stripe;
using Serilog;

namespace ModustaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Ajouter les variables d'environnement
            builder.Configuration.AddEnvironmentVariables();
       
            // Configurer Serilog pour la journalisation
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // charge Serilog section
                .CreateLogger();
   
            builder.Host.UseSerilog(); // <-- Ajout du middleware Serilog

            var stripeSecretKey = Environment.GetEnvironmentVariable("DOTNET_STRIPE_PRIVATE_KEY"); 
            if (string.IsNullOrEmpty(stripeSecretKey))
            {
                stripeSecretKey = builder.Configuration["Stripe:SecretKey"]; // Fallback to appsettings.json si non défini
            }
            StripeConfiguration.ApiKey = stripeSecretKey;

            // Configurer le service Stripe pour être utilisé
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            builder.Services.AddControllers();

            // Ajouter la configuration CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ModustaFrontend", policy =>
                {
                    policy.WithOrigins("https://vue3.modusta.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials(); // si vous utilisez les cookies ou auth header
                });
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Add services to the container.
            builder.Services.Configure<ModustaDatabaseSettings>(builder.Configuration.GetSection("ModustaDatabase"));
            builder.Services.Configure<StripeDatabaseSettings>(builder.Configuration.GetSection("StripeDatabase"));

            builder.Services.AddSingleton<CurriculumService>();
            builder.Services.AddSingleton<UtilisateurService>();
            builder.Services.AddSingleton<ChartService>();
            builder.Services.AddSingleton<StripeService>();

            // Charger les paramètres à partir de appsettings.json et les lier à la classe MySettings
            builder.Services.Configure<Versionneur>(builder.Configuration.GetSection("Version"));
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Kestrel to listen on port 5000
            builder.WebHost.UseUrls("http://*:5000");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Activer la politique CORS configurée
            app.UseCors("AllowAllOrigins");
            app.UseCors("ModustaFrontend");
            app.UseAuthorization();
            //app.MapGet("/health-check", () =>
            //{
            //    var stripeSecretKeyLoaded = builder.Configuration["Stripe:SecretKey"];// != null ? "Yes" : "No";
            //    var connectionStringLoaded = builder.Configuration["ModustaDatabase:ConnectionString"];//!= null ? "Yes" : "No";

            //    return Results.Json(new
            //    {
            //        StripeSecretKeyLoaded = stripeSecretKeyLoaded,
            //        ConnectionStringLoaded = connectionStringLoaded,
            //        MONGODB_CONNECTION_STRING = builder. Configuration,
            //    });
            //});
            app.MapControllers();
            // Vérifier que la valeur secrète a été chargée
            app.Run();
        }
    }
}
