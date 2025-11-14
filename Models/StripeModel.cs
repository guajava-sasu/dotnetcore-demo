using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ModustaAPI.Models
{
    public class StripeModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? SecretKey { get; set; }
        public string? Environment { get; set; }
        public string? WebhookSecret { get; set; }

        public string? PublishableKey { get; set; }

        [BsonElement("successPage")]
        public string? SuccessPage { get; set; }

        [BsonElement("cancelPage")]
        public string? CancelPage { get; set; }

        public StripeModel()
        {
            Id = ObjectId.GenerateNewId();
            /*  SecretKey = String.Empty;
              WebhookSecret = new List<string>();
              SuccessPage = new List<string>();
              CancelPage = new List<Formation>();
              */
        }
    }
}
