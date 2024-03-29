using Lumos.Application.Interfaces.Management;
using Lumos.Data.Models.Management;
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
        private readonly LumosSession _session;
        private readonly IUserAppService _userAppService;

        public AuthController(IConfiguration config, LumosSession session, IUserAppService userAppService)
        {
            _config = config;
            _session = session;
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var loggedInUser = await _userAppService.ValidateUserCredentials(model.UserOrEmail, model.Password);

            if (loggedInUser != null)
            {
                var token = GenerateJwtToken(model.UserOrEmail);

                var response = new
                {
                    token = token,
                    redirectTo = Url.Action("Index", "Home")
                };

                if (loggedInUser.Username == "HOST_ACCESS")
                {
                    _session.SetHostMode(); 
                }
                else
                {
                    _session.SetUserAndTenant(loggedInUser.Id, loggedInUser.TenantId, "Felipe Aparecido Martins");
                }

                // Retorna a resposta JSON
                return Ok(response);
            }

            return BadRequest(new { errorMessage = "Credenciais inválidas. Verifique seus dados e tente novamente." });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _session.Clear();
            var response = new
            {
                redirectTo = Url.Action("Index", "Auth")
            };
            return Ok(response);
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
