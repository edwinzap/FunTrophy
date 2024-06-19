using FunTrophy.API.Settings;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunTrophy.API.Authentication
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly JWTSettings _configuration;
        private readonly IUserRepository _userRepository;

        public JWTManagerRepository(IOptions<JWTSettings> configuration, IUserRepository userRepository)
        {
            _configuration = configuration.Value;
            _userRepository = userRepository;
        }

        public async Task<Token?> Authenticate(AuthenticationUser user)
        {
            var dbUser = await _userRepository.Get(user.UserName, user.Password);
            if (dbUser is null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration.Key);
            var claims = new List<Claim>();
            var userRole = dbUser.IsAdmin ? UserRoles.Admin : UserRoles.User;
            claims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.GivenName, dbUser.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, dbUser.LastName));
            claims.Add(new Claim("userId", dbUser.Id.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_configuration.ExpirationTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token
            {
                AccessToken = tokenHandler.WriteToken(token),
            };
        }
    }
}