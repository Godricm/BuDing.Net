using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuDing.JwtBearer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BuDing.JwtBearer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserDataClient userDataClient;

        public AuthenticateController(IUserDataClient userDataClient)
        {
            this.userDataClient = userDataClient;
        }


        [HttpGet("authenticate")]
        public IActionResult Authenticate([FromBody]User userDto)
        {
            var user = userDataClient.GetByName(userDto.Name);
            if (user == null) return Unauthorized();
            else
            {
                if (user.Password == userDto.Password)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(Consts.SecurityKey);
                    var authTime = DateTime.Now;
                    var expiresAt = authTime.AddMinutes(1);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    { 
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                                 new Claim(ClaimTypes.Name, user.Name),
                                new Claim("id",user.Id.ToString()),
                                new Claim(ClaimTypes.Email,user.Email),
                                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
                            }
                           ),
                        Expires = expiresAt,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    //生成token
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    //写
                    var tokenString = tokenHandler.WriteToken(token);
                    return Ok(
                        new
                        {
                           access_token=tokenString,
                           token_type="Bearer",
                           profile=new
                           {
                               sid=user.Id,
                               name=user.Name,
                               auth=new DateTimeOffset(authTime).ToUnixTimeMilliseconds(),
                               expires_at=new DateTimeOffset(expiresAt).ToUnixTimeMilliseconds()
                           }
                            
                        }
                        );
                }
                else
                {
                    return Unauthorized();
                }
            }

        }


    }
}