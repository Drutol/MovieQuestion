using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieQuestion.Server.Mongo;
using MovieQuestion.Shared.Models;
using MovieQuestion.Shared.Requests;

namespace MovieQuestion.Server.Handlers
{
    public class QueriesHandler :
        IRequestHandler<GetMoviesQuery, List<Movie>>,
        IRequestHandler<GetUserQuery, AppUser>,
        IRequestHandler<GetUserRatingsQuery, List<MovieRating>>
    {
        private readonly IRepository<Movie> _moviewRepository;
        private readonly IRepository<AppUser> _usersRepository;
        private readonly IRepository<MovieRating> _ratingRepository;
        private static Random _random;

        static QueriesHandler()
        {
            _random = new Random();
        }

        public QueriesHandler(
            IRepository<Movie> moviewRepository,
            IRepository<AppUser> usersRepository,
            IRepository<MovieRating> ratingRepository)
        {

            _moviewRepository = moviewRepository;
            _usersRepository = usersRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<List<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            return (await _moviewRepository.GetAllAsync()).ToList();
        }

        public async Task<AppUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.FirstAsync(u => u.Username == request.Username);
            if (user == null)
            {
                user = new AppUser
                {
                    Id = _random.Next(10000, 9999999),
                    Username = request.Username
                };
                await _usersRepository.AddAsync(user);
            }

            return user;
        }

        public async Task<List<MovieRating>> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
        {
            return (await _ratingRepository.WhereAsync(rating => rating.UserId == request.UserId)).ToList();
        }
    }
}
