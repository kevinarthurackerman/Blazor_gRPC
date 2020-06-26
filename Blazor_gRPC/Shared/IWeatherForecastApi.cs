using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blazor_gRPC.Shared
{
    [ServiceContract]
    public interface IWeatherForecastApi
    {
        Task<IEnumerable<WeatherForecast>> Get();
    }

    [DataContract]
    public class WeatherForecast
    {
        [DataMember(Order = 1)]
        public DateTime Date { get; set; }

        [DataMember(Order = 2)]
        public int TemperatureC { get; set; }

        [DataMember(Order = 3)]
        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
