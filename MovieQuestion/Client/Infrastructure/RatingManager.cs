using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Storage;
using MovieQuestion.Client.Utils;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Client.Infrastructure
{
    public class RatingManager
    {
        private readonly LocalStorage _localStorage;
        private readonly ApiCommunicator _apiCommunicator;

        public List<MovieRating> Scores { get; set; } = new List<MovieRating>();

        public RatingManager(LocalStorage localStorage, ApiCommunicator apiCommunicator)
        {
            _localStorage = localStorage;
            _apiCommunicator = apiCommunicator;
        }

        public async Task LoadScores()
        {
            var user = await _localStorage.GetItem<AppUser>();
            Scores = await _apiCommunicator.GetUserRatings(user.Id);
        }

        public bool TryGetRating(Movie movie, out MovieRating rating)
        {
            rating = Scores.FirstOrDefault(movieRating => movieRating.MovieId == movie.Id);
            return rating != null;
        }

        public void SetRating(MovieRating rating)
        {
            var existingRating = Scores.FirstOrDefault(movieRating => movieRating.MovieId == rating.MovieId);
            if (existingRating != null)
                Scores.Remove(existingRating);
            Scores.Add(rating);
        }
    }
}
