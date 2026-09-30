using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TokenServices
{
    private readonly IConfiguration _config;
    private readonly UserManager<AppUser> _userManager;

    public TokenServices(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<string> GenerateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!)
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    var tokernHandler = new JwtSecurityTokenHandler();
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSecurityKey:SecretKey"]!));

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
        Issuer = "rsf.com",
        Audience = "abc"
    };

    var token = tokernHandler.CreateToken(tokenDescriptor);
        return tokernHandler.WriteToken(token);
}
}