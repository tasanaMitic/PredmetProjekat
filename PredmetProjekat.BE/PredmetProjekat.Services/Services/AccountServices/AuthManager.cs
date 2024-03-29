﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PredmetProjekat.Common.Constants;
using PredmetProjekat.Common.Dtos.IdentityDtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PredmetProjekat.Services.Services.AccountServices
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _configuration;
        private Account _user;
        private IConfigurationSection _jwtSettings;

        public AuthManager(UserManager<Account> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(Constants.Jwt);
        }
        public async Task<string> CreateToken()
        {
            var signedCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signedCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signedCredentials, List<Claim> claims)
        {
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection(Constants.Lifetime).Value));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection(Constants.Issuer).Value,
                claims: claims,
                expires: expires,
                signingCredentials: signedCredentials
                );

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim> { new Claim(Constants.Username, _user.UserName) };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(Constants.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _jwtSettings.GetSection(Constants.Key).Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginDto loginDto)
        {
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            return _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
        }

        public string DecodeToken(string tokenString)
        {
            var jwtEncodedString = tokenString.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString);

            return token.Claims.First(x => x.Type == Constants.Username).Value;
        }
    }
}
