using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace QuizCore.Modules.UserModule.Identities;

public class JwtTokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(JwtConfig config)
    {
        _jwtConfig = config;

    }

    public string GenerateJSONWebToken(User user, List<Claim> claims)
    {
        List<Claim> userClaims=new();
        var securityKey = GetSymmetricSecurityKey();
        var tokenExpiresAt = GetTokenExpiresDate();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        if(claims.Count > 0)
        {
            userClaims.AddRange(claims);
        }
        userClaims.Add(new Claim(ClaimTypes.Name,user.UserName!));
        userClaims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName!));
            userClaims.Add(new Claim("TokenExpireDateTime", tokenExpiresAt.ToString()));
            System.Console.WriteLine("Claims: " + System.Text.Json.JsonSerializer.Serialize(userClaims));
        var token = new JwtSecurityToken(_jwtConfig.Issuer,
            null,
            userClaims,
            expires: tokenExpiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    public DateTime GetTokenExpiresDate()
    {
        return DateTime.UtcNow.AddHours(_jwtConfig.ExpireInHours);
    }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
    }
}
