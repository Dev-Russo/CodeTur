using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Queries.Usuario;
using System.Linq;

namespace CodeTur.Dominio.Handlers.Queries.Usuario
{
    public class BuscarUsuarioPorIdQueryHandler : IHandlerQuery<BuscarUsuarioPorIdQuery>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public BuscarUsuarioPorIdQueryHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(BuscarUsuarioPorIdQuery query)
        {
            var usuario = _repositorio.BuscarPorId(query.IdUsuario);
            var retorno = new BuscarUsuarioPorIdQueryResult();

            retorno.Id = usuario.Id;
            retorno.Nome = usuario.Nome;
            retorno.Email = usuario.Email;
            retorno.Telefone = usuario.Telefone;
            retorno.QuantidadeComentarios = usuario.Comentarios.Count;
            retorno.Comentarios = usuario.Comentarios.ToList();

            return new GenericCommandResult(true, "Dados do usuário", retorno);
        }

        IQueryResult IHandlerQuery<BuscarUsuarioPorIdQuery>.Handle(BuscarUsuarioPorIdQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}