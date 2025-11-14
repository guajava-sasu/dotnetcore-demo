using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ModustaAPI.Models
{
    public class Utilisateur
    {
        public Utilisateur()
        {
    
        }
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("sub")]
        public string? Sub { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("nickname")]
        public string? Nickname { get; set; }

        [BsonElement("given_name")]
        public string? GivenName { get; set; }

        [BsonElement("family_name")]
        public string? FamilyName { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("email_verified")]
        public bool EmailVerified { get; set; }

        [BsonElement("picture")]
        public string? Picture { get; set; }

        [BsonElement("locale")]
        public string? Locale { get; set; }

        [BsonElement("identities")]
        public List<Identity>? Identities { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("app_metadata")]
        public AppMetadata? AppMetadata { get; set; }

        [BsonElement("user_metadata")]
        public UserMetadata? UserMetadata { get; set; }

        [BsonElement("abonnement")]
        public Abonnement? Abonnement { get; set; }
    }

    public class Identity
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("provider")]
        public string? Provider { get; set; }

        [BsonElement("user_id")]
        public string? UserId { get; set; }

        [BsonElement("connection")]
        public string? Connection { get; set; }

        [BsonElement("isSocial")]
        public bool IsSocial { get; set; }
    }

    public class AppMetadata
    {
        [BsonElement("plan")]
        public string? Plan { get; set; }

        [BsonElement("quota")]
        public int Quota { get; set; }
    }

    public class UserMetadata
    {
        [BsonElement("preferences")]
        public Preferences? Preferences { get; set; }
    }

    public class Preferences
    {
        [BsonElement("theme")]
        public string? Theme { get; set; }
    }
}