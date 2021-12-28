using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.DataContracts.Repository
{
    public interface IWeatherExternalRepository
    {
        Task<RepositoryResponse<WeatherCityResponse>> GetAvailableCities(CancellationToken cancellationToken);

        Task<RepositoryResponse<WeatherDataResponse>> GetWeatherForCity(string city, CancellationToken cancellationToken);
    }
}