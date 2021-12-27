using MetaApp.DataContracts.Domain;

namespace MetaApp.DataContracts.Command.Response
{
    public class UpsertWeatherDataHandlerResponse
    {
        public bool IsSuccess { get; set; }
        public WeatherDataModel Weather { get; set; }
    }
}