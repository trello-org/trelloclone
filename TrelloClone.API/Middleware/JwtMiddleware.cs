using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Dtos;
using TrelloClone.Utils;

namespace TrelloClone.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientServiceImplementation _clientService;
        private readonly TokenValidationParameters _params;
        //private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IHttpClientServiceImplementation clientService)
        {
            _next = next;
            _clientService = clientService;
            _params = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mylittlesecretkeyneedstobelongenough")),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            };
        }

        public async Task Invoke(HttpContext context, UserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			Console.WriteLine(token + "****");
            bool isProtected = OpenURLCollection.IsUrlProtected(context.Request.Path);
            if (isProtected && token != null)
			{
				Console.WriteLine("Attaching user to context");
               await AttachUserToContext(context, userService, token);

			}

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, UserService userService, string token)
        {
			var validatedToken = await ValidateTokenOrNull(token, context, _clientService, _params);
			Console.WriteLine($"User with refresh token  {context.Request.Cookies["refreshToken"]}");
            if (validatedToken == null)
                return;

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetByIdAsync(userId);
           //context.Request.Cookies.SingleOrDefault(c => c.Key == "refreshToken").Value = jwtToken.
        }

        public static async Task<SecurityToken> ValidateTokenOrNull(string token, HttpContext context, IHttpClientServiceImplementation clientService, TokenValidationParameters parameters)
        {
            SecurityToken validatedToken = null;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
			{
                tokenHandler.ValidateToken(token, parameters, out validatedToken);
            }
            catch (SecurityTokenExpiredException)
			{
                var cookie = context.Request.Cookies["refreshToken"];

                Console.WriteLine(cookie);
				Console.WriteLine("Refreshing token..");
                var ret = await clientService.RefreshToken(cookie);
                context.Request.Headers.Add("Cookie", $"refreshToken={cookie};");
                return tokenHandler.ReadJwtToken(ret.Token);
                /*tokenHandler.ValidateToken(ret.Token, parameters, out validatedToken.SecToken);
                validatedToken.hasTokenBeenRefreshed = true;
                validatedToken.TokenPair = ret;*/
            }
            catch (Exception)
			{
                return null;
			}

            return validatedToken;
        }

    }
}

