using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.AppSetting;
using WebApi.Response;

namespace WebApi.Controllers
{ 
    public class ApiBaseController : ControllerBase
    {
        public IServiceWrapper serviceWrapper;
        public AppSettings appSettings;
        public ISession session;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public ApiBaseController(IServiceWrapper service, IOptions<AppSettings> appSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            this.serviceWrapper = service;
            this.appSettings = appSettings.Value;

            // config session
            this._iHttpContextAccessor = iHttpContextAccessor;
            this.session = _iHttpContextAccessor.HttpContext.Session;
            // end
        }
             
        // create token
        [NonAction]
        public async Task<TokenModel> GerenateToken(NguoiDungViewModel nguoidung)
        {
            try
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.UTF8.GetBytes(appSettings.SecretKey);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, nguoidung.HoTen), 
                        new Claim(JwtRegisteredClaimNames.Email, nguoidung.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserName", nguoidung.UserName),
                        new Claim("Id", nguoidung.ID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(20),// thời gian sử dụng token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha512Signature)
                };

                // create token + refreshToken
                var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
                var accessToken = jwtSecurityTokenHandler.WriteToken(token);
                var refreshToken = GenerateRefreshToken();

                // save token to database
               await serviceWrapper.RefreshToken.Add(new RefreshTokenViewModel { 
                    Id = Guid.NewGuid(),
                    JwtId = token.Id,
                    Token = refreshToken,
                    IsUsed = false,
                    IsRevoked = false,
                    IssuedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddHours(1)
                });

                return new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                return new TokenModel();
            }
        }

        // create refreshToken 32 bit
        [NonAction]
        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
    }
}
