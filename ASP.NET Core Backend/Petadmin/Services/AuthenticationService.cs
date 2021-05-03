using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Identity.Jwt;
using Petadmin.Identity.Jwt.Interfaces;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            ITokenFactory tokenFactory, IJwtFactory jwtFactory, IJwtTokenValidator jwtTokenValidator, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
            _jwtTokenValidator = jwtTokenValidator;
            _logger = logger;
        }

        public async Task<RegisterResponse> RegisterAsync(AuthenticateRequest model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                throw new UserAlreadyExistException();

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Gender = model.Gender,
                LockoutEnd = DateTimeOffset.MinValue,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new RegistrationFailedException();

            var createdUser = await _userManager.FindByNameAsync(user.UserName);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new ApplicationRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Doctor))
                await _roleManager.CreateAsync(new ApplicationRole(UserRoles.Doctor));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Assistant))
                await _roleManager.CreateAsync(new ApplicationRole(UserRoles.Assistant));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRolesAsync(createdUser, new List<string>() { UserRoles.Admin, UserRoles.Doctor });
            }

            return new RegisterResponse
            {
                Success = true
            };
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password, string remoteIpAddress)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password)) return null;

            var refreshToken = _tokenFactory.GenerateToken();
            user.AddRefreshToken(refreshToken, user.Id, remoteIpAddress, 7);
            await _userManager.UpdateAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            AccessToken accessToken = _jwtFactory.GenerateEncodedToken(userRoles, user.UserName);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                User = new ApplicationUserMysqlResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Email = user.Email,
                    License = user.License,
                    PhoneNumber = user.PhoneNumber,
                }
            };
        }

        public async Task<RefreshAccessTokenResponse> RefreshAccessTokenAsync(RefreshAccessTokenRequest request)
        {
            ClaimsPrincipal cp = _jwtTokenValidator.GetPrincipalFromToken(request.AccessToken);
            
            // invalid token/signing key was passed and we can't extract user claims
            if (cp != null)
            {
                var userName = cp.Claims.First().Value;
                var user = await _userManager.FindByNameAsync(userName);
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                if (user.HasValidRefreshToken(request.RefreshToken))
                {
                    var jwtToken = _jwtFactory.GenerateEncodedToken(userRoles, userName);

                    return new RefreshAccessTokenResponse
                    {
                        AccessToken = jwtToken,
                        User = new ApplicationUserMysqlResponse
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Gender = user.Gender,
                            Email = user.Email,
                            License = user.License,
                            PhoneNumber = user.PhoneNumber,
                        }
                    };
                }
            }
            return null;
        }

        public async Task<bool> RevokeTokenAsync(RevokeTokenRequest request)
        {
            ClaimsPrincipal cp = _jwtTokenValidator.GetPrincipalFromToken(request.AccessToken);
            
            // invalid token/signing key was passed and we can't extract user claims
            if (cp != null)
            {
                var userName = cp.Claims.First().Value;
                var user = await _userManager.FindByNameAsync(userName);

                if (user.HasRefreshToken(request.RefreshToken))
                {
                    user.RemoveRefreshToken(request.RefreshToken); // delete the token 
                    await _userManager.UpdateAsync(user);

                    return true;
                }
            }
            return false;
        }
    }
}
