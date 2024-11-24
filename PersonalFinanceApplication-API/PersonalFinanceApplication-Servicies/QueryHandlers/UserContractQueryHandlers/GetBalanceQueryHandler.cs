using Grpc.Core;
using Grpc.Net.Client;
using gRPCClient;
using MediatR;
using Microsoft.Extensions.Options;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PFA_gRPCClient.ServiceProperties;

namespace PersonalFinanceApplication_Services.QueryHandlers.UserContractQueryHandlers
{
    public class GetBalanceQuery : IRequest<AccountBalanceDto>
    {
        public int UserContractId { get; set; }
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, AccountBalanceDto>
    {
        private readonly gRPCSettings _gRPCSettings;
        public GetBalanceQueryHandler(IOptions<gRPCSettings> options)
        {
            _gRPCSettings = options.Value;
        }

        public async Task<AccountBalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress(_gRPCSettings.AccountBalanceServiceEndpoint);
            var client = new AccountBalanceService.AccountBalanceServiceClient(channel);
            var accountBalanceRequest = new AccountBalanceRequest()
            {
                UserContractId = request.UserContractId
            };

            try
            {
                var respone = client.GetAccountBalance(accountBalanceRequest);
                return respone.MapAcccountBalanceRequest();
            }
            catch (RpcException exception)
            {
                throw new CoreException(exception, "No account balance found");
            }
        }
    }
}
