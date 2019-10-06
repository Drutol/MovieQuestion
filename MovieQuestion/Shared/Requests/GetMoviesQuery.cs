using System.Collections.Generic;
using MediatR;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Shared.Requests
{
    public class GetMoviesQuery : IRequest<List<Movie>>
    {
    }
}
