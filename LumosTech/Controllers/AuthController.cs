using Lumos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lumos.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginVM model)
        {
            if (model.UserOrEmail == "admin" && model.Password == "123")
            {
                var token = GenerateJwtToken(model.UserOrEmail);

                var response = new
                {
                    token = token,
                    redirectTo = Url.Action("Index", "Home")
                };

                // Retorna a resposta JSON
                return Ok(response);
            }

            return BadRequest("Credenciais inválidas");
        }

        private string GenerateJwtToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:JwtBearer:SecurityKey"]));

            if (securityKey.KeySize < 256)
            {
                var newKeyBytes = new byte[32]; // Tamanho em bytes para 256 bits
                Array.Copy(securityKey.Key, newKeyBytes, securityKey.Key.Length);
                securityKey = new SymmetricSecurityKey(newKeyBytes);
            }

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Authentication:JwtBearer:Issuer"],
                audience: _config["Authentication:JwtBearer:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("RenewToken")]
        public IActionResult RenewToken(string tokenFixed)
        {
            if (tokenFixed == "8ee6fc18-ff62-408d-b785-c8d73efbab00")
            {
                var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest("Usuário não autenticado");
                }

                var token = GenerateJwtToken(userEmail);

                return Ok(new { token });
            }

            return BadRequest();
        }
    }
}
