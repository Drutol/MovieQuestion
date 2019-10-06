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
    public class CommandsHandler : IRequestHandler<SetMovieRatingCommand>
    {
        private readonly IRepository<AppUser> _userRepository;
        private IRepository<MovieRating> _ratingRepository;

        public CommandsHandler(
            IRepository<AppUser> userRepository,
            IRepository<MovieRating> ratingRepository)
        {
            _userRepository = userRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<Unit> Handle(SetMovieRatingCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.FirstAsync(user => user.Id == request.MovieRating.MovieId) != null)
                return Unit.Value;

            var existingRating = await _ratingRepository.FirstAsync(rating =>
                rating.UserId == request.MovieRating.MovieId && rating.MovieId == request.MovieRating.UserId);

            if (existingRating != null)
            {
                await _ratingRepository.Remove(existingRating);
            }

            await _ratingRepository.AddAsync(request.MovieRating);

            return Unit.Value;
        }
    }
}
