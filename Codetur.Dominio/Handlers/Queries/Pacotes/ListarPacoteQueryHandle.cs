using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Queries.Pacote;
using System.Linq;

namespace CodeTur.Dominio.Handlers.Queries.Pacote
{
    public class ListarPacoteQueryHandler : IHandlerQuery<ListarPacoteQuery>
    {
        private readonly IPacoteRepositorio _repositorio;

        public ListarPacoteQueryHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        public ICommandResult Handle(ListarPacoteQuery query)
        {
            var pacotes = _repositorio.Listar(query.Ativo);

            var Pacotes = pacotes.Select(
                x =>
                {
                    return new ListarPacotesQueryResult()
                    {
                        Id = x.Id,
                        Titulo = x.Titulo,
                        Descricao = x.Descricao,
                        Ativo = x.Ativo,
                        Imagem = x.Imagem,
                        QuantidadeComentarios = x.Comentarios.Count
                    };
                }
            );

            return new GenericCommandResult(true, "Usuários", Pacotes);
        }

        IQueryResult IHandlerQuery<ListarPacoteQuery>.Handle(ListarPacoteQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
