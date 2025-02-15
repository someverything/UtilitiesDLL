﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Entities;
using UtilitiesDLL.Security.Abstract;

namespace UtilitiesDLL.Security.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenManager(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<Token> CreateAccessToken(AppUser appUser, List<string> roles)
        {
            Token token = new Token();
            var claims = new List<Claim>()
            { 
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, appUser.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            token.ExpirationDate = DateTime.UtcNow.AddMinutes(2);
            JwtSecurityToken securityToken = new(
                                issuer: _configuration["Token:Audience"],
                audience: _configuration["Token:Issuer"],
                expires: token.ExpirationDate,
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            token.AccessToken = handler.WriteToken(securityToken);
            token.RefreshTokem = CreateRefreshToken();
            foreach (var claim in claims)
            {
                await _userManager.AddClaimAsync(appUser, claim);
            };
            
            return token;
        
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
