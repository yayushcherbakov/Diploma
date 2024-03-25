using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuditService.ApplicationServices.Options;
using AuditService.ApplicationServices.Utilities;
using AuditService.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuditService.ApplicationServices.Extensions;

public static class JwtExtensions
{
    public static List<Claim> CreateClaims(this ApplicationUser user, List<IdentityRole<Guid>> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, string.Join(" ", roles.Select(x => x.Name))),
        };
        return claims;
    }

    public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, JwtOptions jwtOptions)
    {
        return new(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.Expire),
            signingCredentials: JwtHelpers.CreateSigningCredentials(jwtOptions)
        );
    }
}
