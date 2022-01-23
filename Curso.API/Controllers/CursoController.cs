using curso.api.Models;
using Curso.API.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos ativos do usuário.
        /// </summary>
        /// <returns>Retorna status ok e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            /* var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value); */
            var cursos = new List<CursoViewModelOutput>()
            {
                new CursoViewModelOutput()
                {
                    Login = "",
                    Descricao = "teste",
                    Nome = "teste"
                },
            };

            return Ok(cursos);
        }
    }
}
