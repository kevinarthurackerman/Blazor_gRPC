using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor_gRPC.Shared;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace Blazor_gRPC.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(x =>
            {
                var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
                var baseAddress = x.GetRequiredService<HttpClient>().BaseAddress;
                return GrpcChannel.ForAddress(baseAddress, new GrpcChannelOptions() { HttpClient = new HttpClient(handler) });
            });
                
            builder.Services.AddSingleton(x =>
            {
                var grpcChannel = x.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<IWeatherForecastApi>();
            });

            await builder.Build().RunAsync();
        }
    }
}
