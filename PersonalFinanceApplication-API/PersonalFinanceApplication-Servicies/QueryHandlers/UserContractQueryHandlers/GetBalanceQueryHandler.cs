using Grpc.Core;
using gRPCClient;
using MediatR;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.GrpcServiceConnection;
using PersonalFinanceTracker_Contracts.FinancialTrackerContracts;

namespace PersonalFinanceApplication_Services.QueryHandlers.UserContractQueryHandlers
{
    public class GetBalanceQuery : IRequest<AccountBalanceDto>
    {
        public int UserContractId { get; set; }
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, AccountBalanceDto>
    {
        private readonly IGrpcServiceConnection _grpcServiceConnection;
        public GetBalanceQueryHandler(IGrpcServiceConnection grpcServiceConnection)
        {
            _grpcServiceConnection = grpcServiceConnection;
        }

        public async Task<AccountBalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var channel = _grpcServiceConnection.GetGrpcClient();

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
                throw new CoreException(exception.Message);
            }
        }
    }
}
