using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Exceptions;
using Petadmin.Models;
using Petadmin.Services.Interfaces;
using Response = Petadmin.Models.Response;

namespace Petadmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRegisterService _registerService;
        
        public AccountController(IAuthenticationService authenticationService, IRegisterService registerService)
        {
            _authenticationService = authenticationService;
            _registerService = registerService;
        }

        // GET api/account/register
        [HttpGet]
        [Route("register")]
        public async Task<ActionResult<RegisterPage>> GetRegisterPageAsync()
        {
            try
            {
                RegisterPage data = await _registerService.GetRegisterPageAsync();
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // POST api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateRequest model)
        {
            try
            {
                await _authenticationService.RegisterAsync(model);
                return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User Created Successfully" });
            }
            catch (UserAlreadyExistException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = e.Message });
            }
            catch (RegistrationFailedException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticateRequest model)
        {
            try
            {
                string remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
                LoginResponse response = await _authenticationService.LoginAsync(model.UserName, model.Password, remoteIpAddress);
             
                if (response == null) 
                    return Unauthorized();

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/account/refresh-token
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshAccessTokenRequest request)
        {
            try
            {
                request.RemoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
                RefreshAccessTokenResponse token = await _authenticationService.RefreshAccessTokenAsync(request);
                if (token == null)
                    return Unauthorized();

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/account/revoke-token
        [HttpPost]
        [Route("revoke-token")]
        public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            try
            {
                bool ans = await _authenticationService.RevokeTokenAsync(request);
                return Ok(ans);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
