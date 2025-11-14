using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModustaAPI.Controllers;
using ModustaAPI.Models;
using MongoDB.Driver;
using Serilog;
using System.Linq;
using System.Security.Cryptography;

namespace ModustaAPI.Services
{
    public class CurriculumService
    {
        //  private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Curriculum> _booksCollection;
        private readonly UtilisateurService _userService;
        private readonly ILogger<CurriculumService> _logger;
        public CurriculumService(
            IOptions<ModustaDatabaseSettings> bookStoreDatabaseSettings,
            UtilisateurService userService,
            ILogger<CurriculumService> logger
            //, IConfiguration configuration
            )
        {
            _logger = logger;

            //  _configuration = configuration;
            var connectionString = Environment.GetEnvironmentVariable("DOTNET_MONGODB_CONNECTION_STRING")
?? bookStoreDatabaseSettings.Value.ConnectionString;
            Log.Logger.Information("ConnectionString utilisée: " + Environment.GetEnvironmentVariable("DOTNET_MONGODB_CONNECTION_STRING"));
            Log.Logger.Information("ConnectionString utilisée (connectionString): " + connectionString);

            var mongoClient = new MongoClient(connectionString);
            //  var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Curriculum>(bookStoreDatabaseSettings.Value.BooksCollectionName);
            _userService = userService;
        }

        public async Task<List<Curriculum>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<List<IdentificationCv>> GetAllIdAsync()
        {
            var result = new List<IdentificationCv>();
            try
            {
                _logger.LogInformation("Récupération de tous les CVs dans le service");            // Récupération de la liste de documents
                var tmp = await _booksCollection.Find(_ => true).ToListAsync();

                // Transformation de la liste de documents en une liste d'Entete
                result = tmp.Select(x => new IdentificationCv
                {
                    Id = x.Id.ToString(),
                    Etiquette = x.Etiquette.ToString()
                }).ToList();
                _logger.LogInformation($"Nombre de CVs: {result.Count}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoSomething");
            }
            return result;
        }

        public async Task<List<IdentificationCv>> GetAllIdAsync(string utilisateur)
        {
            _logger.LogInformation("----------****-----ConnectionString utilisée: " + Environment.GetEnvironmentVariable("DOTNET_MONGODB_CONNECTION_STRING"));

            _logger.LogInformation($"on entre dans GetAllIdAsync");
            var result = new List<IdentificationCv>();
            try
            {
                var user = await _userService.GetAsync(utilisateur);
                // Récupération de la liste de documents
                //   var tmp0 = await _booksCollection.Find(_ => true).ToListAsync();
                var cvFound = await _booksCollection.Find(x => x.Utilisateur.ToString().Contains(user.Sub.ToString())).ToListAsync();

                if (cvFound == null || !cvFound.Any())
                {
                    _logger.LogInformation($"Aucun CV trouvé pour l'utilisateur {utilisateur} dans GetAllIdAsync");
                    return result;
                }
                _logger.LogInformation($"{cvFound.Count} CVs trouvé pour l'utilisateur {utilisateur} dans GetAllIdAsync");
                // Transformation de la liste de documents en une liste d'Entete
                result = cvFound.Select(x => new IdentificationCv
                {
                    Id = x.Id.ToString(),
                    Etiquette = x.Etiquette.ToString()
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoSomething");
            }
            return result;
        }

        public async Task<Curriculum?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();

        public async Task<Curriculum?> GetByUserAsync(string id) =>
       await _booksCollection.Find(x => x.Utilisateur.ToString() == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Curriculum newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Curriculum updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id.ToString() == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id.ToString() == id);
    }
}
