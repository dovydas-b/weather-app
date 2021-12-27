namespace MetaApp.DataContracts.Repository
{
    public class WeatherDataResponse
    {
        public string City { get; set; }

        public double Temperature { get; set; }

        public string Weather { get; set; }

        public double Precipitation { get; set; }
    }
}