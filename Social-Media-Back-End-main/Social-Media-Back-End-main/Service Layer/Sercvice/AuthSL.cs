using CommonLayer.Models;
using Repository_Layer.Interface;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Sercvice
{
    public class AuthSL : IAuthSL
    {
        private readonly IAuthRL _authRL;
        public AuthSL(IAuthRL authRL)
        {
            _authRL = authRL;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            return await _authRL.SignIn(request);
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            return await _authRL.SignUp(request);
        }
    }
}
