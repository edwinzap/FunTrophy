using System.Security.Claims;
using System.Text.Json;

namespace FunTrophy.Web.Authentication
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var keyValuePairs = GetKeyValuePairsFromJwt(jwt);

            if (keyValuePairs is null)
                return claims;

            ExtractRolesFromJWT(claims, keyValuePairs);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private static Dictionary<string, object>? GetKeyValuePairsFromJwt(string jwt)
        {
            if (string.IsNullOrWhiteSpace(jwt))
                return null;
            
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs;
        }

        public static DateTime? GetExpirationDateFromJwt(string jwt)
        {
            var keyValuePairs = GetKeyValuePairsFromJwt(jwt);
            return ExtractExpirationDateFromJWT(keyValuePairs);
        }

        private static DateTime? ExtractExpirationDateFromJWT(Dictionary<string, object>? keyValuePairs)
        {
            if (keyValuePairs is null)
                return null;
            
            keyValuePairs.TryGetValue("exp", out object? jwtExpirationDate);
            if (jwtExpirationDate is not null)
            {
                if (int.TryParse(jwtExpirationDate.ToString(), out int seconds))
                {
                    return DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
                }
            }
            return null;
        }

        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue("role", out object? roles);

            if (roles is not null)
            {
                var parsedRoles = roles.ToString()!.Trim().TrimStart('[').TrimEnd(']').Split(',');

                if (parsedRoles.Length > 1)
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;

                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}