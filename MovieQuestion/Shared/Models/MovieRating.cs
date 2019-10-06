using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieQuestion.Shared.Models
{
    public class MovieRating : IEntity
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public long UserId { get; set; }
        public long MovieId { get; set; }
        public int Score { get; set; }
    }
}
