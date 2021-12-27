using System.ComponentModel.DataAnnotations;
using CommandDotNet;

namespace MetaApp.Console.RequestModel
{
    public class GetWeatherDataRequest : IArgumentModel
    {
        [Required,  Option("city", Split = ',')]
        public string[] Cities { get; set; }
    }
}