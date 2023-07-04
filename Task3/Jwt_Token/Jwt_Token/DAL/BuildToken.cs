using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Jwt_Token.DAL
{
    public class BuildToken
    {
        readonly IConfiguration _configuration;

        public BuildToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken()
        {
            var bytes = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwtconfig:Key").Value);
            //var bytes = Encoding.UTF8.GetBytes("IT'S A TRIAL JWT TOKEN INTEGRATION");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration.GetSection("Jwtconfig:Issuer").Value, audience: _configuration.GetSection("Jwtconfig:Issuer").Value, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            //JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
