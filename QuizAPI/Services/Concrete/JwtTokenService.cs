using Microsoft.IdentityModel.Tokens;
using QuizAPI.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizAPI.Services.Concrete
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string name, string surname, string userName, List<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var claims = new List<Claim>()
            {
                new Claim("UserName", userName),
                new Claim("Name", name),
                new Claim("Surname", surname),
               
            };

            if (roles != null && roles.Any())
            {
                // Add role claims only if user has roles
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256), 
                claims: claims);
            var jwt= new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

      
    }
}
