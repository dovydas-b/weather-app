namespace MetaApp.DataContracts
{
    public class WeatherDto
    {
        public string City { get; set; }

        public double? Temperature { get; set; }

        public string Weather { get; set; }

        public double? Precipitation { get; set; }
    }
}