using System.Threading;
using System.Threading.Tasks;
using MetaApp.DataContracts;
using MetaApp.DataContracts.Api;
using MetaApp.DataContracts.Repository;

namespace MetaApp.Infrastructure.Contracts
{
    public interface IWeatherApiClient
    {
        Task<ApiResponse<WeatherDataResponse>> GetWeatherForCity(string city, CancellationToken cancellationToken);

        Task<ApiResponse<WeatherCityResponse>> GetAvailableCities(CancellationToken cancellationToken);
    }
}