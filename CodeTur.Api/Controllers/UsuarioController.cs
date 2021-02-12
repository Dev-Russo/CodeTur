using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Entidades;
using CodeTur.Comum.Commands;
using CodeTur.Dominio.Commands.Usuario;
using CodeTur.Dominio.Handlers.Queries.Usuario;
using CodeTur.Dominio.Handlers.Usuario;
using CodeTur.Dominio.Queries.Usuario;
using CodeTur.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CodeTur.Api.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public UsuarioController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("users")]
        [HttpGet]
        public GenericCommandResult GetAll(
            [FromServices] ListarUsuarioQueryHandler handler
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var query = new ListarUsuarioQuery();
            return (GenericCommandResult)handler.Handle(query);
        }

        [Route("users/{id}")]
        [HttpGet]
        public GenericCommandResult GetByIdUser(Guid id,
            [FromServices] BuscarUsuarioPorIdQueryHandler handler
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            var query = new BuscarUsuarioPorIdQuery();
            query.IdUsuario = id;
            return (GenericCommandResult)handler.Handle(query);
        }

        [Route("signup")]
        [HttpPost]
        public GenericCommandResult Register(
            [FromBody] CriarContaCommand command,
            [FromServices] CriarContaCommandHandler handler
        )
        {
            //command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("signin")]
        [HttpPost]
        public GenericCommandResult SignIn(
            [FromBody] LogarCommand command,
            [FromServices] LogarCommandHandler handler
        )
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            if (resultado.Sucesso)
            {
                Usuario usuario = (Usuario)resultado.Data;
                var token = new Token(
                                        Configuration["Token:issuer"],
                                        Configuration["Token:audience"],
                                        Configuration["Token:secretKey"]
                                     )
                                     .GerarJsonWebToken(
                                        usuario.Id,
                                        usuario.Nome,
                                        usuario.Email,
                                        usuario.TipoUsuario.ToString()
                                     );

                return new GenericCommandResult(true, "Usuário logado", new { token = token });
            }

            return resultado;
        }

        [Route("reset-password")]
        [HttpPut]
        public GenericCommandResult ResetPassword(
            [FromBody] ResetarSenhaCommand command,
            [FromServices] ResetarSenhaCommandHandler handler
        )
        {
            var resultado = (GenericCommandResult)handler.Handle(command);

            return resultado;
        }

        [Route("update-password")]
        [Authorize]
        [HttpPut]
        public GenericCommandResult UpdatePassword(
           [FromBody] AlterarSenhaCommand command,
           [FromServices] AlterarSenhaCommandHandler handler
       )
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);

            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("")]
        [HttpPut]
        public GenericCommandResult UpdateAccount(
           [FromBody] AlterarUsuarioCommand command,
           [FromServices] AlterarUsuarioCommandHandler handler
       )
        {
            var idUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            command.IdUsuario = new Guid(idUsuario.Value);

            return (GenericCommandResult)handler.Handle(command);
        }


    }
}