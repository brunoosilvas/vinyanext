using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Infrastructure.Authentication;

internal sealed class TokenProvider(
    IConfiguration configuration,
    IDataProtectionProvider dataProtectionProvider,
    IDistributedCache cache
    ) : ITokenProvider
{
    public async Task<(string token, string refreshTotken)> Create(GsisUsuario usuario)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var protectorKey = dataProtectionProvider.CreateProtector(secretKey);

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                //revisar
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, string.Empty)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);

        DistributedCacheEntryOptions cacheOptions = new()
        {
            AbsoluteExpiration = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes") * 2)
        };

        await cache.SetStringAsync(
            configuration.GetValue<string>("Redis:Keys:Login"),
            JsonConvert.SerializeObject(null),
            cacheOptions
        );

        string refreshToken = Guid.NewGuid().ToString();

        return (token, refreshToken);
    }

}
