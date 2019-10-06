using MongoDB.Bson;

namespace MovieQuestion.Shared.Models
{
    public interface IEntity
    {
        ObjectId ObjectId { get; set; }
    }
}
