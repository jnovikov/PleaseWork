using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BackendApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace BackendApi.Controllers
{
    public class AuthController : Controller
    {
        private MyContext db;

        public AuthController(MyContext db)
        {
            this.db = db;
        }

        [HttpPost("/register")]
        public IActionResult GetLogin()
        {
            var username = Request.Form["email"];
            var password = Request.Form["password"];

            if ((username == "") || (password == ""))
            {
                return BadRequest("Введите логин и пароль");
            }

            var user = db.Users.FirstOrDefault(x => x.Email == username);
            if (user != null)
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            return Ok($"Вы успешно зарегестрировались");
        }


        [HttpPost("/login")]
        public async Task Token()
        {
            var username = Request.Form["email"];
            var password = Request.Form["password"];

            var identity = getIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Неправильный email или пароль");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings {Formatting = Formatting.Indented}));
        }

        private ClaimsIdentity getIdentity(string email, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                };
                var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return identity;
            }

            return null;
        }
    }
}