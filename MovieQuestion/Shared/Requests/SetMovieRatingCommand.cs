using MediatR;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Shared.Requests
{
    public class SetMovieRatingCommand : IRequest
    {
        public MovieRating MovieRating { get; set; }
    }
}
