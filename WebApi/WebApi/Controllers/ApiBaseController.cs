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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.AppSetting;

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
         
        [NonAction]
        public string GetCurrentUser()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == "family_name").Value;
                if (!string.IsNullOrEmpty(email))
                {
                    email = email.ToLower().Replace("v.", string.Empty);
                    var matches = Regex.Matches(email, @"([^@]+)");
                    return $"{matches[0].Groups[0].Value.ToLower()}@vingroup.net";
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [NonAction]
        public string GetCurrentUserFromToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == "family_name").Value;
                if (!string.IsNullOrEmpty(email))
                {
                    email = email.ToLower().Replace("v.", string.Empty);
                    var matches = Regex.Matches(email, @"([^@]+)");
                    return $"{matches[0].Groups[0].Value.ToLower()}@vingroup.net";
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [NonAction]
        public (string, bool) GetCurrentUserAndCheckExpired()
        {
            bool isExpired = false;
            try
            {

                var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
                if (tokenS.ValidTo < DateTime.UtcNow.AddMinutes(1))
                {
                    isExpired = true;
                }
                var email = tokenS.Claims.First(claim => claim.Type == "family_name").Value;
                if (!string.IsNullOrEmpty(email))
                {
                    email = email.ToLower().Replace("v.", string.Empty);
                    var matches = Regex.Matches(email, @"([^@]+)");
                    return ($"{matches[0].Groups[0].Value.ToLower()}@vingroup.net", isExpired);
                }
                else
                    return (string.Empty, isExpired);
            }
            catch (Exception ex)
            {
                return (string.Empty, isExpired);
            }
        }

        [NonAction]
        public string GetBearerToken()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].ToString();
                if (accessToken.IndexOf("Bearer") == -1) accessToken = $"Bearer {accessToken}";
                return accessToken;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
         
        [NonAction]
        public string GetFullNameCurrentUser()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken);
                var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;
                var fullName = tokenS.Claims.First(claim => claim.Type == "name").Value;
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName = fullName.Split(new char[] { '(' })[0].Trim();
                    return fullName;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
         
        [NonAction]
        public string GetToken()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                return accessToken;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [NonAction]
        public string GerenateToken(NguoiDungViewModel nguoidung)
        {
            try
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.UTF8.GetBytes(appSettings.SecretKey);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, nguoidung.HoTen),
                        new Claim(ClaimTypes.Email, nguoidung.Email),
                        new Claim("Username", nguoidung.UserName),
                        new Claim("ID", nguoidung.ID.ToString()),
                        new Claim("TokenID", Guid.NewGuid().ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha512Signature)
                };

                var token = jwtSecurityToken.CreateToken(tokenDescription);

                return jwtSecurityToken.WriteToken(token);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
