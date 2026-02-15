using MediatR;
using PersonalFinanceApplication_DTO.DtoModels;

namespace PersonalFinanceApplication_Services.CommandHandlers.SalarySchedulerHandlers
{
    public class CreateSalarySchedulerCommand : IRequest<Guid>
    {
        public SalarySchedulerDto SalarySchedulerDto { get; set; }
    }

    public class CreateSalarySchedulerHandler : IRequestHandler<CreateSalarySchedulerCommand, Guid>
    {
        public Task<Guid> Handle(CreateSalarySchedulerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
