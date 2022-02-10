using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TrelloClone.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			Console.WriteLine(token + "****");
            if (token != null)
			{
				Console.WriteLine("Attaching user to context");
               await AttachUserToContext(context, userService, token);

			}

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, UserService userService, string token)
        {
			SecurityToken validatedToken = ValidateTokenOrNull(token);

            if (validatedToken == null)
                return;

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetByIdAsync(userId);
        }

        public static SecurityToken ValidateTokenOrNull(string token)
        {
            SecurityToken validatedToken;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
			{
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mylittlesecretkeyneedstobelongenough")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);
            }
            catch (Exception)
			{
                return null;
			}

            return validatedToken;
        }
    }
}

