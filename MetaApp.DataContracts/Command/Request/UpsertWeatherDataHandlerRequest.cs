using MediatR;
using MetaApp.DataContracts.Command.Response;

namespace MetaApp.DataContracts.Command.Request
{
    public class UpsertWeatherDataHandlerRequest : IRequest, IRequest<UpsertWeatherDataHandlerResponse>
    {
        public string CityName { get; set; }
    }
}