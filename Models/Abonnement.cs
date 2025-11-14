using MongoDB.Bson.Serialization.Attributes;

namespace ModustaAPI.Models
{
    public class Abonnement
    {
        [BsonElement("statut")]
        public string? Statut { get; set; }  // "actif", "annulé", "en attente"

        [BsonElement("type")]
        public string? Type { get; set; }  // "mensuel", "annuel", "hebdomadaire"

        [BsonElement("date_debut")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateDebut { get; set; }

        [BsonElement("date_fin")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateFin { get; set; }

        [BsonElement("transaction_id")]
        public string? TransactionId { get; set; }

        [BsonElement("transaction_type")]
        public string? TransactionType { get; set; }  // "paypal", "stripe", "autre", "..."
    }
}
