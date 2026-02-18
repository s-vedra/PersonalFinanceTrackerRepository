using FluentValidation;
using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.Mappers;
using PersonalFinanceApplication_Services.HelperMethods;

namespace PersonalFinanceApplication_Services.CommandHandlers.SalarySchedulerHandlers
{
    public class CreateSalarySchedulerCommand : IRequest<Guid>
    {
        public SalarySchedulerDto SalarySchedulerDto { get; set; }
    }

    public class CreateSalaryScheduleValidator : AbstractValidator<CreateSalarySchedulerCommand>
    {
        public CreateSalaryScheduleValidator()
        {
            RuleFor(salaryScheduler => salaryScheduler.SalarySchedulerDto.Amount).NotEmpty().NotNull();
            RuleFor(salaryScheduler => salaryScheduler.SalarySchedulerDto.UserContractId).NotEmpty().NotNull();
            RuleFor(salaryScheduler => salaryScheduler.SalarySchedulerDto.DayOfMonth).NotNull().NotEmpty().NotEqual(0);
        }
    }
    public class CreateSalarySchedulerHandler : IRequestHandler<CreateSalarySchedulerCommand, Guid>
    {
        private readonly IScheduledSalaryRepository _salaryScheduleRepository;
        private readonly IUserContractRepository _userContractRepository;

        public CreateSalarySchedulerHandler(IScheduledSalaryRepository salaryScheduleRepository, IUserContractRepository userContractRepository)
        {
            _salaryScheduleRepository = salaryScheduleRepository;
            _userContractRepository = userContractRepository;
        }
        public async Task<Guid> Handle(CreateSalarySchedulerCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSalaryScheduleValidator();
            validator.ValidateAndThrow(request);
            var userContract = _userContractRepository.GetEntity(request.SalarySchedulerDto.UserContractId);
            if (!userContract.IsNull())
            {
                var salaryScheduler = request.SalarySchedulerDto.ToModel();
                _salaryScheduleRepository.AddEntity(salaryScheduler);
                return salaryScheduler.ReferenceId;
            }
            throw new CoreException("User contract cannot be found");
        }
    }
}
