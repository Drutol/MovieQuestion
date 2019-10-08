using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieQuestion.Shared.Models;
using MovieQuestion.Shared.Requests;

namespace MovieQuestion.Client.Infrastructure
{
    public class ApiCommunicator
    {
#if DEBUG
        public const string ApiUrl = "http://localhost:55307";
#else
        public const string ApiUrl = "https://mylovelyvps.xyz/movies";
#endif


        private readonly HttpClient _httpClient;

        public ApiCommunicator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var json = await _httpClient.GetStringAsync($"{ApiUrl}/movie");
                return JsonSerializer.Deserialize<List<Movie>>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Movie>();
            }
        }

        public async Task<List<MovieRating>> GetUserRatings(long userId)
        {
            try
            {
                var json = await _httpClient.GetStringAsync($"{ApiUrl}/rating/{userId}");
                return JsonSerializer.Deserialize<List<MovieRating>>(json);
            }
            catch (Exception e)
            {
                return new List<MovieRating>();
            }
        }

        public async Task<AppUser> GetUser(string username)
        {
            try
            {
                var json = await _httpClient.GetStringAsync($"{ApiUrl}/user?username={username}");
                return JsonSerializer.Deserialize<AppUser>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddRating(MovieRating rating)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{ApiUrl}/rating",
                    new StringContent(JsonSerializer.Serialize(new SetMovieRatingCommand
                    {
                        MovieRating = rating
                    }), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
