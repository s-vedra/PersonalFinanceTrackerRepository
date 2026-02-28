using Grpc.Net.Client;

namespace PersonalFinanceApplication_Services.GrpcServiceConnection
{
    public interface IGrpcServiceConnection
    {
        public GrpcChannel GetGrpcClient();
    }
}
