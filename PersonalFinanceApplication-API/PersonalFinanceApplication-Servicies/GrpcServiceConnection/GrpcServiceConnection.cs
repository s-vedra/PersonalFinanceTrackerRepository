using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using PersonalFinanceApplication_Services.HelperMethods;
using PFA_gRPCClient.ServiceProperties;

namespace PersonalFinanceApplication_Services.GrpcServiceConnection
{
    public class GrpcServiceConnection : IGrpcServiceConnection
    {
        private readonly IEnvironmentValidationService _environmentValidationService;
        private readonly gRPCSettings _gRPCSettings;
        public GrpcServiceConnection(IEnvironmentValidationService environmentValidationService, IOptions<gRPCSettings> options)
        {
            _environmentValidationService = environmentValidationService;
            _gRPCSettings = options.Value;
        }

        public GrpcChannel GetGrpcClient()
        {
            var isInDocker = _environmentValidationService.IsDocker();

            var channelOptions = !isInDocker
            ? new GrpcChannelOptions()
            : new GrpcChannelOptions
            {
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                },
                Credentials = ChannelCredentials.Insecure
            };

            return GrpcChannel.ForAddress(_gRPCSettings.AnalyzingServiceEndpoint, channelOptions);
        }
    }
}
