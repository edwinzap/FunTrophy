using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunTrophy.API.Authentication
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private List<User> Users = new()
        {
            new User{ Id = 1, IsAdmin = true, Password = "admin", UserName = "admin" },
            new User{ Id = 2, IsAdmin = false, Password = "user", UserName = "user" },
        };

        private readonly IConfiguration _configuration;
        private readonly TimeSpan _tokenExpirationTime = TimeSpan.FromHours(12);

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token? Authenticate(AuthenticationUser user)
        {
            var dbUser = Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (dbUser is null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var claims = new List<Claim>();
            var userRole = dbUser.IsAdmin ? UserRoles.Admin : UserRoles.User;
            claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_tokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { AccessToken = tokenHandler.WriteToken(token) };
        }
    }
}