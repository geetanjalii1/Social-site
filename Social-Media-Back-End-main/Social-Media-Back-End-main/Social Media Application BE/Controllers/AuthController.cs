using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media_Application_BE.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthSL _authSL;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthSL authSL, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _authSL = authSL;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                _logger.LogInformation($"SignUp Calling In AdminController.... Time : {DateTime.Now}");
                response = await _authSL.SignUp(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("Exception Occur In AuthController : Message : ", ex.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                _logger.LogInformation($"SignIn Calling In AdminController.... Time : {DateTime.Now}");
                response = await _authSL.SignIn(request);
                if (response.IsSuccess)
                {
                    response = await CreateToken(response, "User Authentication");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("Exception Occur In AuthController : Message : ", ex.Message);

            }

            return Ok(response);
        }

        private async Task<SignInResponse> CreateToken(SignInResponse request, string Type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("UserName", request.data.UserName.ToString()));
                claims.Add(new Claim("UserId", request.data.UserId.ToString()));
                claims.Add(new Claim("TokenType", Type));

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audiance"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                request.data.Token = new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                request.IsSuccess = false;
                request.Message = "Exception Occur In Token Creation : Message : " + ex.Message;
                _logger.LogError("Exception Occur In Token Creation : Message : ", ex.Message);
            }
            return request;
        }

    }
}
