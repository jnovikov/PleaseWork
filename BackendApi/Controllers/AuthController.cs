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
using BackendApi.Helpers;

namespace BackendApi.Controllers
{
    public class AuthController : BaseController
    { 
        [HttpPost("/register")]
        public IActionResult Register()
        {
            var email = Request.Form["email"].ToString();
            var password = Request.Form["password"].ToString();
            var name = Request.Form["name"].ToString();


            if ((email == "") || (password == "") || (name == ""))
            {
                return BadRequest(new {message = "Пожалуйста, заполните все поля"});
            }


            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                return BadRequest(new {message = "Пользователь с таким email уже зарегестрирован"});
            }
            password = PasswordHash.GetHash(password);
            db.Users.Add(new User {Email = email, Password = password, Name = name});
            db.SaveChanges();

            return Ok(new {message = "Вы успешно зарегистрировались"});
        }


        [HttpPost("/login")]
        public async Task Token()
        {
            var username = Request.Form["email"].ToString();
            var password = Request.Form["password"].ToString();

            password = PasswordHash.GetHash(password);

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