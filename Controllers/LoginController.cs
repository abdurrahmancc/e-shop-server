using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_shop_server.Helper;
using e_shop_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop_server.Controllers
{
     [ApiController]
    [Route("v1/api/Login")]
    public class LoginController : ControllerBase
    {
        
        private readonly JwtService _jwtService;

        public LoginController( JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginInfo)
        {
            var tokenValue = _jwtService.GenerateJwtToken(loginInfo.Email);

            return Ok(new { Token = tokenValue });
        }
    }
}