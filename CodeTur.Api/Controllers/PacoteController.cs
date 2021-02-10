using Codetur.Dominio.Commands.Pacotes;
using Codetur.Dominio.Handlers.Pacotes;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Enum;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Handlers.Pacotes;
using CodeTur.Dominio.Queries.Pacote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public GenericCommandResult Create(CriarPacoteCommand command,
                                           [FromServices] CriarPacoteCommandHandle handle)
        {
            return (GenericCommandResult)handle.Handle(command);
        }


        [HttpGet]
        [Authorize]
        public GenericQueryResult GetAll([FromServices] ListarPacoteQueryHandle handle)
        {
            ListarPacotesQuery query = new ListarPacotesQuery();

            var tipoUsuario = HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Role);

            if (tipoUsuario.Value.ToString() == EnTipoUsuario.Comum.ToString())
                query.Ativo = true;

            return (GenericQueryResult)handle.Handle(query);
        }

    }
}
