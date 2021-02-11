using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Queries.Usuario;
using System.Linq;

namespace CodeTur.Dominio.Handlers.Queries.Usuario
{
    public class ListarUsuarioQueryHandler : IHandlerQuery<ListarUsuarioQuery>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public ListarUsuarioQueryHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(ListarUsuarioQuery command)
        {
            var query = _repositorio.Listar();

            var usuarios = query.Select(
                x =>
                {
                    return new ListarQueryResult()
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Email = x.Email,
                        Telefone = x.Telefone,
                        TipoUsuario = x.TipoUsuario.ToString(),
                        QuantidadeComentarios = x.Comentarios.Count
                    };
                }
            );

            return new GenericCommandResult(true, "Usuários", usuarios);
        }

        IQueryResult IHandlerQuery<ListarUsuarioQuery>.Handle(ListarUsuarioQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
