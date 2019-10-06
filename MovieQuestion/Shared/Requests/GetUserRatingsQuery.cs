using System.Collections.Generic;
using MediatR;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Shared.Requests
{
    public class GetUserRatingsQuery : IRequest<List<MovieRating>>
    {
        public long UserId { get; set; }
    }
}
