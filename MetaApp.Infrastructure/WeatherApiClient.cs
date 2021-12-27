using System.Threading;
using System.Threading.Tasks;
using MetaApp.DataContracts;
using MetaApp.DataContracts.Api;
using MetaApp.DataContracts.Configuration;
using MetaApp.DataContracts.Repository;
using MetaApp.Infrastructure.Contracts;
using Microsoft.Extensions.Options;
using RestSharp;

namespace MetaApp.Infrastructure
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly IRestClient restClient;
        private readonly WeatherApiConfiguration apiConfiguration;

        public WeatherApiClient(IOptions<WeatherApiConfiguration> options)
        {
            this.restClient = new RestClient(options.Value.BaseUrl);
            apiConfiguration = options.Value;
        }

        public async Task<ApiResponse<WeatherCityResponse>> GetAvailableCities(CancellationToken cancellationToken)
        {
            var authResponse = await this.Authenticate(cancellationToken);

            if (!authResponse.IsSuccessful)
            {
                return new ApiResponse<WeatherCityResponse>
                {
                    IsSuccess = false
                };
            }

            var request = new RestRequest("Cities")
                .AddHeader("Authorization", $"bearer {authResponse.Data.Bearer}");

            var response = await restClient.ExecuteGetAsync<WeatherCityResponse>(request, cancellationToken);

            return new ApiResponse<WeatherCityResponse>
            {
                Data = response.Data,
                IsSuccess = response.IsSuccessful
            };
        }

        public async Task<ApiResponse<WeatherDataResponse>> GetWeatherForCity(string city, CancellationToken cancellationToken)
        {
            var authResponse = await this.Authenticate(cancellationToken);

            if (!authResponse.IsSuccessful)
            {
                return new ApiResponse<WeatherDataResponse>
                {
                    IsSuccess = false
                };
            }

            var request = new RestRequest($"Weather/{city}")
                .AddHeader("Authorization", $"bearer {authResponse.Data.Bearer}");

            var response = await restClient.ExecuteGetAsync<WeatherDataResponse>(request, cancellationToken);

            return new ApiResponse<WeatherDataResponse>
            {
                Data = response.Data,
                IsSuccess = response.IsSuccessful
            };
        }

        private Task<IRestResponse<AuthResponse>> Authenticate(CancellationToken cancellationToken)
        {
            var request = new RestRequest("authorize")
                .AddJsonBody(new BasicAuthRequest
                {
                    Password = this.apiConfiguration.Password,
                    Username = this.apiConfiguration.Username,
                });

            return restClient.ExecutePostAsync<AuthResponse>(request, cancellationToken);
        }
    }
}