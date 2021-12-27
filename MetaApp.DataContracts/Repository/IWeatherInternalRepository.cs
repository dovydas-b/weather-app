using System.Threading.Tasks;
using MetaApp.DataContracts.Domain;

namespace MetaApp.DataContracts.Repository
{
    public interface IWeatherInternalRepository
    {
        Task<RepositoryResponse<WeatherDataModel>> Insert(WeatherDataModel weatherData);

        Task<RepositoryResponse<WeatherDataModel>> Update(string id, WeatherDataModel weatherData);

        Task<RepositoryResponse<WeatherDataModel>> GetByCity(string city);
    }
}