using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ModustaAPI.Models;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace ModustaAPI.Services
{
    public class UtilisateurService
    {
        private readonly IMongoCollection<Utilisateur> _userCollection;

        public UtilisateurService(
            IOptions<ModustaDatabaseSettings> modustaStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                modustaStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                modustaStoreDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<Utilisateur>(
                modustaStoreDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<Utilisateur>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<List<string>> GetAllSubAsync()
        {
            var result = new List<string>();
            var tmp = await _userCollection.Find(_ => true).ToListAsync();
            result = tmp.Select(x => x.Sub.ToString()).ToList();
            return result;
        }

        public async Task<Utilisateur?> GetAsync(string Sub) =>
            await _userCollection.Find(x => x.Sub != null && x.Sub.ToString() == Sub).FirstOrDefaultAsync();

        public async Task CreateAsync(Utilisateur newBook) =>
            await _userCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string Sub, Utilisateur updatedBook) =>
            await _userCollection.ReplaceOneAsync(x => x.Sub != null && x.Sub.ToString() == Sub, updatedBook);

        public async Task RemoveAsync(string Sub) =>
            await _userCollection.DeleteOneAsync(x => x.Sub != null && x.Sub.ToString() == Sub);

        public async Task UpdateUserSubscription(string UserId, bool isActive, DateTime expirationDate)
        {
            var user = await _userCollection.Find(x => x.Sub != null && x.Sub.Equals(UserId)).FirstAsync();
            if (user != null)
            {
                if (user.Abonnement == null)
                {
                    user.Abonnement = new Abonnement
                    {
                        DateDebut = DateTime.Now,
                        DateFin = expirationDate,
                        Statut = "Actif",
                        TransactionType = "Stripe"
                    };
                };
                //  user.IsSubscribed = isActive;
                //  user.SubscriptionExpiryDate = expirationDate;
                await _userCollection.ReplaceOneAsync(x => x.Sub != null && x.Sub.ToString() == UserId, user);
            }
        }
    }
}
