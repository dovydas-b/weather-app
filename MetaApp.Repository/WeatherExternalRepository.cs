using MetaApp.DataContracts.Repository;
using MetaApp.Infrastructure.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.Repository
{
    public class WeatherExternalRepository : IWeatherExternalRepository
    {
        private readonly IWeatherApiClient weatherApiClient;

        public WeatherExternalRepository(IWeatherApiClient weatherApiClient)
        {
            this.weatherApiClient = weatherApiClient;
        }

        public async Task<RepositoryResponse<WeatherCityResponse>> GetAvailableCities(CancellationToken cancellationToken)
        {
            var apiResult = await weatherApiClient.GetAvailableCities(cancellationToken);

            return new RepositoryResponse<WeatherCityResponse>
            {
                Data = apiResult.Data,
                IsSuccess = apiResult.IsSuccess
            };
        }

        public async Task<RepositoryResponse<WeatherDataResponse>> GetWeatherForCity(string city, CancellationToken cancellationToken)
        {
            var apiResult = await weatherApiClient.GetWeatherForCity(city, cancellationToken);

            return new RepositoryResponse<WeatherDataResponse>
            {
                Data = apiResult.Data,
                IsSuccess = apiResult.IsSuccess
            };
        }
    }
}