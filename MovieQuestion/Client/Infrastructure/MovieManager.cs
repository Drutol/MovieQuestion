using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieQuestion.Shared.Models;

namespace MovieQuestion.Client.Infrastructure
{
    public class MovieManager
    {
        private readonly ApiCommunicator _apiCommunicator;

        public List<Movie> Movies { get; set; }

        public MovieManager(ApiCommunicator apiCommunicator)
        {
            _apiCommunicator = apiCommunicator;
        }

        public async Task LoadMovies()
        {
            Movies = await _apiCommunicator.GetMovies();
        }
    }
}
