using System.ComponentModel.DataAnnotations;
using CommandDotNet;

namespace MetaApp.RequestModel
{
    public class GetWeatherDataRequest : IArgumentModel
    {
        [Required,  Option("city", Split = ',')]
        public string[] Cities { get; set; }
    }
}