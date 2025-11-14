using Microsoft.Extensions.Options;
using ModustaAPI.Models;
using MongoDB.Driver;

namespace ModustaAPI.Services
{
    public class StripeService
    {
        private readonly IMongoCollection<StripeModel> _stripeCollection;

        public StripeService(IOptions<StripeDatabaseSettings> bookStoreDatabaseSettings)
        {
            var connectionString =
                Environment.GetEnvironmentVariable("DOTNET_MONGODB_CONNECTION_STRING")
                ?? bookStoreDatabaseSettings.Value.ConnectionString;

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

            _stripeCollection = mongoDatabase.GetCollection<StripeModel>(
                bookStoreDatabaseSettings.Value.StripeCollectionName);
        }

        public async Task<StripeModel> GetAsync()
        {
            return await _stripeCollection.Find(_ => true).FirstAsync();
        }
    }
}
