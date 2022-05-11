namespace StoryBooks.Features.Identity.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtTokenGeneratorService : IAuthTokenGeneratorService
{
    private readonly IConfiguration configuration;

    public JwtTokenGeneratorService(IConfiguration configuration)
        => this.configuration = configuration;

    public TokenModel GenerateToken(User user, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string? secret = this.configuration["Authentication:Secret"];
        byte[]? key = Encoding.ASCII.GetBytes(secret);
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

        foreach (string? role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        string? encryptedToken = tokenHandler.WriteToken(token);

        return new TokenModel(encryptedToken, tokenDescriptor.Expires);
    }
}