using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieQuestion.Shared.Models
{
    public class AppUser : IEntity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId ObjectId { get; set; }

        public long Id { get; set; }
        public string Username { get; set; }
    }
}
