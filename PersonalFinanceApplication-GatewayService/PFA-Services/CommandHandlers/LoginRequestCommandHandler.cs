using MediatR;
using PFA_DTOModels.Commands;
using PFA_DTOModels.DTOModels;
using PFA_Services.Abstractions;

namespace PFA_Services.CommandHandlers
{
    public class LoginRequestCommand : IRequest<LoginResponseModel>
    {
        public LoginRequestModels LoginRequestDto { get; set; }
    }

    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponseModel>
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IJwtAuthService _jwtAuthService;
        public LoginRequestCommandHandler(IFirebaseAuthService firebaseAuthService, IJwtAuthService jwtAuthService)
        {
            _firebaseAuthService = firebaseAuthService;
            _jwtAuthService = jwtAuthService;
        }

        public async Task<LoginResponseModel> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _firebaseAuthService.GetCurrentUser(request.LoginRequestDto);
            var firebaseToken = await _firebaseAuthService.FirebaseCustomToken(currentUser);

            var jwtRequest = new JwtRequest()
            {
                UserId = currentUser.Uid,
                Email = currentUser.Email
            };
            var jwtToken = _jwtAuthService.GenerateToken(jwtRequest);

            return new LoginResponseModel()
            {
                FirebaseToken = firebaseToken,
                InternalJwt = jwtToken
            };
        }
    }
}
