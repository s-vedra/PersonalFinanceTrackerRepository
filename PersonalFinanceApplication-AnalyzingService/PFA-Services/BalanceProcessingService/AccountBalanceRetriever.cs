using Grpc.Core;
using gRPCClient;
using PFA_DAL.Abstraction;
using PFA_Exceptions.Exceptions;
using PFA_Mappers.Mappers;
using PFA_Services.HelperMethods;

namespace PFA_Services.BalanceProcessingService
{
    public class AccountBalanceRetriever : AccountBalanceService.AccountBalanceServiceBase
    {
        private readonly IAccountBalanceRepository _accountBalanceRepository;
        public AccountBalanceRetriever(IAccountBalanceRepository accountBalanceRepository)
        {
            _accountBalanceRepository = accountBalanceRepository;
        }

        public override Task<AccountBalanceResponse> GetAccountBalance(AccountBalanceRequest accountBalanceRequest,
            ServerCallContext context)
        {
            var accountBalance = _accountBalanceRepository.GetEntity(accountBalanceRequest.UserContractId);
            if (!accountBalance.IsNull())
            {
                return Task.FromResult(accountBalance.MapAccountBalanceRequest());
            }
            throw new CoreException("No account balance found");
        }
    }
}
