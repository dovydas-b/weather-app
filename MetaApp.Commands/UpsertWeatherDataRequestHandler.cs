using MediatR;
using MetaApp.DataContracts;
using MetaApp.DataContracts.Command.Request;
using MetaApp.DataContracts.Command.Response;
using MetaApp.DataContracts.Domain;
using MetaApp.DataContracts.Repository;
using MetaApp.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.Commands
{
    public class UpsertWeatherDataRequestHandler : IRequestHandler<UpsertWeatherDataHandlerRequest, UpsertWeatherDataHandlerResponse>, IRequest<UpsertWeatherDataHandlerResponse>
    {
        private readonly IWeatherExternalRepository weatherExternalRepository;
        private readonly IWeatherInternalRepository weatherRepository;
        private readonly IMessageQueue messageQueue;
        private readonly ILogger<UpsertWeatherDataRequestHandler> logger;

        public UpsertWeatherDataRequestHandler(
            IWeatherExternalRepository weatherExternalRepository,
            IWeatherInternalRepository weatherRepository,
            IMessageQueue messageQueue,
            ILogger<UpsertWeatherDataRequestHandler> logger)
        {
            this.weatherExternalRepository = weatherExternalRepository;
            this.weatherRepository = weatherRepository;
            this.messageQueue = messageQueue;
            this.logger = logger;
        }

        public async Task<UpsertWeatherDataHandlerResponse> Handle(UpsertWeatherDataHandlerRequest request,
            CancellationToken cancellationToken)
        {
            var weatherResponse = await weatherExternalRepository.GetWeatherForCity(request.CityName, cancellationToken);

            if (!weatherResponse.IsSuccess)
            {
                messageQueue.Queue(new WeatherDto
                {
                    City = request.CityName,
                }, cancellationToken);

                logger.LogWarning($"City not found {request.CityName}");

                return new UpsertWeatherDataHandlerResponse
                {
                    IsSuccess = false
                };
            }

            var weatherDataModel = new WeatherDataModel
            {
                Id = Guid.NewGuid().ToString(),
                City = weatherResponse.Data.City,
                Precipitation = weatherResponse.Data.Precipitation,
                Temperature = weatherResponse.Data.Temperature,
                Weather = weatherResponse.Data.Weather
            };

            var cityWeatherDataResponse = await weatherRepository.GetByCity(request.CityName);

            var upsertRepositoryResponse = cityWeatherDataResponse.IsSuccess
                ? await weatherRepository.Update(cityWeatherDataResponse.Data.Id, weatherDataModel)
                : await weatherRepository.Insert(weatherDataModel);

            if (!upsertRepositoryResponse.IsSuccess)
            {
                return new UpsertWeatherDataHandlerResponse
                {
                    IsSuccess = false
                };
            }

            messageQueue.Queue(new WeatherDto
            {
                Weather = weatherDataModel.Weather,
                City = weatherDataModel.City,
                Precipitation = weatherDataModel.Precipitation,
                Temperature = weatherDataModel.Temperature
            }, cancellationToken);

            return new UpsertWeatherDataHandlerResponse
            {
                Weather = upsertRepositoryResponse.Data,
                IsSuccess = true
            };
        }
    }
}