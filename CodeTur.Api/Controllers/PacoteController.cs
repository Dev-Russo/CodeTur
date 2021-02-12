using CodeTur.Comum.Commands;
using CodeTur.Comum.Enum;
using CodeTur.Dominio.Commands.Pacote;
using CodeTur.Dominio.Handlers.Pacote;
using CodeTur.Dominio.Handlers.Queries.Pacote;
using CodeTur.Dominio.Queries.Pacote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace CodeTur.Api.Controllers
{
    [Route("v1/package")]
    [ApiController]
    public class PacoteController : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public GenericCommandResult Create(
            [FromBody] AdicionarPacoteCommand command,
            [FromServices] AdicionarPacoteHandler handler
        )
        {
            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public GenericCommandResult Update(Guid id,
            [FromBody] AlterarPacoteCommand command,
            [FromServices] AlterarPacoteHandler handler
        )
        {
            if (id == Guid.Empty)
                return new GenericCommandResult(false, "Informe o Id do Pacote", "");

            command.IdPacote = id;

            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPut("{id}/image")]
        [Authorize(Roles = "Admin")]
        public GenericCommandResult UpdateImage(Guid id,
            [FromBody] AlterarImagemPacoteCommand command,
            [FromServices] AlterarImagemHandler handler
        )
        {
            if (id == Guid.Empty)
                return new GenericCommandResult(false, "Informe o Id do Pacote", "");

            command.IdPacote = id;

            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public GenericCommandResult UpdateStatus(Guid id,
            [FromBody] AlterarStatusCommand command,
            [FromServices] AlterarStatusHandler handler
        )
        {
            if (id == Guid.Empty)
                return new GenericCommandResult(false, "Informe o Id do Pacote", "");

            command.IdPacote = id;

            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpGet()]
        [Authorize]
        public GenericCommandResult GetAll([FromServices] ListarPacoteQueryHandler handle)
        {
            ListarPacoteQuery query = new ListarPacoteQuery();

            var tipoUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (tipoUsuario.Value.ToString() == EnTipoUsuario.Comum.ToString())
                query.Ativo = true;


            return (GenericCommandResult)handle.Handle(query);
        }

        [HttpGet("{id}")]
        [Authorize]
        public GenericCommandResult GetById(Guid id, [FromServices] BuscarPacotePorIdQueryHandler handle)
        {
            BuscarPacotePorIdQuery query = new BuscarPacotePorIdQuery();

            var tipoUsuario = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            query.IdPacote = id;
            query.TipoUsuario = (EnTipoUsuario)Enum.Parse(typeof(EnTipoUsuario), tipoUsuario.Value);

            return (GenericCommandResult)handle.Handle(query);
        }
    }
}

