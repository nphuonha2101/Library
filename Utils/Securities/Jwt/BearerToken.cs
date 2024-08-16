using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Library.Utils.Securities;

public class BearerToken(IConfiguration configuration)
{
    private readonly IConfiguration configuration = configuration;
    
    /**
     * Generate a JWT token for the user.
     */
    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "customer"),
                new Claim(ClaimTypes.AuthenticationMethod, "password")
            },
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:Timeout"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}