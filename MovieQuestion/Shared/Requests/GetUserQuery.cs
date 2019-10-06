using MediatR;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Shared.Requests
{
    public class GetUserQuery : IRequest<AppUser>
    {
        public string Username { get; set; }
    }
}
