using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.AuthLogin;

public class LoginAuthCommand : IRequest<AccessToken>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class AuthLoginCommandHandler : IRequestHandler<LoginAuthCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public AuthLoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<AccessToken> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.AuthLoginEmailCheck(request.Email);
            User user = await _userRepository.GetAsync(u => u.Email == request.Email);

            if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("The password you entered is incorrect.");

            IPaginate<UserOperationClaim> userGetClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
                include: i => i.Include(i => i.OperationClaim));

            AccessToken accessToken = _tokenHelper.CreateToken(user, userGetClaims.Items.Select(u => u.OperationClaim).ToList());
            return accessToken;
        }
    }
}