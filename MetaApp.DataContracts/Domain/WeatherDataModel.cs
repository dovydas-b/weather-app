using System;

namespace MetaApp.DataContracts.Domain
{
    public class WeatherDataModel
    {
        public string Id { get; set; }

        public string City { get; set; }

        public double Temperature { get; set; }

        public string Weather { get; set; }

        public double Precipitation { get; set; }
    }
}