using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieQuestion.Shared.Models
{
    public class Movie : IEntity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId ObjectId { get; set; }

        public long Id { get; set; }
        public long ExternalId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Tagline { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
