using System;
using System.Linq;
using JsonFlatFileDataStore;
using MetaApp.DataContracts.Domain;
using System.Threading.Tasks;
using MetaApp.DataContracts.Repository;

namespace MetaApp.Repository
{
    public class WeatherInternalRepository : IWeatherInternalRepository
    {
        private readonly IDataStore dataStore;
        private const string CollectionName = "weather";

        public WeatherInternalRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task<RepositoryResponse<WeatherDataModel>> Insert(WeatherDataModel weatherData)
        {
            var isSuccess = await this.dataStore
                .GetCollection(CollectionName)
                .InsertOneAsync(weatherData);

            return new RepositoryResponse<WeatherDataModel>
            {
                IsSuccess = isSuccess,
                Data = weatherData
            };
        }

        public Task<RepositoryResponse<WeatherDataModel>> GetByCity(string city)
        {
            var response = this.dataStore
                .GetCollection<WeatherDataModel>(CollectionName)
                .AsQueryable()
                .FirstOrDefault(x => x.City.Equals(city, StringComparison.InvariantCultureIgnoreCase));

            return Task.FromResult(new RepositoryResponse<WeatherDataModel>
            {
                IsSuccess = response != null,
                Data = response
            });
        }

        public async Task<RepositoryResponse<WeatherDataModel>> Update(string id, WeatherDataModel weatherData)
        {
            var isSuccess = await this.dataStore
                .GetCollection<WeatherDataModel>(CollectionName)
                .UpdateOneAsync(id, weatherData);

            return new RepositoryResponse<WeatherDataModel>
            {
                Data = weatherData,
                IsSuccess = isSuccess
            };
        }
    }
}