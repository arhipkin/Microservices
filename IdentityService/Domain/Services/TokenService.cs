using Core.Interfaces;
using Core.Models;
using Core.Models.Entities;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenSettings _tokenSettings;

        public TokenService(
            IUserRepository userRepository,
            IOptions<TokenSettings> tokenSettings)
        {
            _userRepository = userRepository;
            _tokenSettings = tokenSettings?.Value ?? throw new ArgumentNullException(nameof(TokenSettings));
        }

        public async Task<TokenInfo> GenerateTokenAsync(AppUser appUser)
        {
            var roles = await _userRepository.GetUserRolesAsync(appUser);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.UserName)
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var certificate = new X509Certificate2(_tokenSettings.PrivateCertPath, _tokenSettings.PrivateCertPassword);
            var securityKey = new X509SecurityKey(certificate);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Subject = new ClaimsIdentity(authClaims),
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now + _tokenSettings.Expiration,
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.RsaSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return new TokenInfo
            {
                Token = token,
                Expiration = securityToken.ValidTo.ToLocalTime()
            };
        }
    }
}
