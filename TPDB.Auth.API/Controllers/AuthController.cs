using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TPDB.Auth.API.Data;
using TPDB.Auth.API.Models;
using TPDB.Auth.Common;

namespace TPDB.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TPDBAuthContext db;
        private readonly IOptions<AuthOptions> _authOptions;

        //Инжектируем контекст и опции AuthOptions из библиотеки Auth.Common
        public AuthController(TPDBAuthContext context, IOptions<AuthOptions> authOptions)
        {
            db = context;
            _authOptions = authOptions;
        }

        //Login endpoint
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Login request)
        {
            //"Аутентификация" пользователя по эмэйлу и паролю из реквеста 
            var user = await AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {
                //Генерация JWT-токена на основе данных пользователя
                var token = GenerateJWT(user);

                //Возвращаем созданный JWT-токен
                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        //Метод аутентификации пользователя
        private async Task<Account> AuthenticateUser(string email, string password)
        {
            //Поиск в базе на основе эмэйла и пароля,
            //таблица ролей инклюдится с целью создания клэйма ролей в дальнейшем
            return await db.Users.Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Password == password && u.Email == email);
        }

        //Метод генерации JWT-токена на основе данных пользователя
        private string GenerateJWT(Account user)
        {
            //Определение параметров аутентификации как опций AuthOptions
            var authParams = _authOptions.Value;

            //Генерация SymetricSecurityKey для JWT
            var securityKey = authParams.GetSymetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Создание клэимов для пользователя (создается клэим эмэйла и sub-клэим)
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            //Добавление авторизационных клэимов (клэимов ролей)
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.Name));
            }

            //Определение token типа JWTSecurityToken
            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}