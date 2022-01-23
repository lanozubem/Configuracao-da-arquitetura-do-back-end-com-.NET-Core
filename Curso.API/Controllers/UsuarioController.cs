using curso.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModeOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            /* if(!ModelState.IsValid) return BadRequest(new ValidaCampoViewModeOutput(ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage))); */
            var usuarioViewModelOutput = new UsarioViewModelOutput()
            {
                Codigo = 1,
                Login = "zubemluano",
                Email = "zubemluano@gmail.com"
            };

            var secret = Encoding.ASCII.GetBytes("Mzbhduhduasd$#@#$%¨¨&%¨dssbdjhbas(@@$))(!!@)(()dj)()jjhssjhsa");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var SecurityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(SecurityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        [HttpPost]
        [Route("registrar")]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {
            return Created("", registroViewModelInput);
        }
    }
}
