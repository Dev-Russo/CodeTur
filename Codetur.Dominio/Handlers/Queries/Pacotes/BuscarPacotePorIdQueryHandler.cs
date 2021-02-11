using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Enum;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Queries;
using CodeTur.Dominio.Entidades;
using CodeTur.Dominio.Queries.Pacote;
using System.Collections.Generic;
using System.Linq;

namespace CodeTur.Dominio.Handlers.Queries.Pacote
{
    public class BuscarPacotePorIdQueryHandler : IHandlerQuery<BuscarPacotePorIdQuery>
    {
        private readonly IPacoteRepositorio _repositorio;

        public BuscarPacotePorIdQueryHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(BuscarPacotePorIdQuery query)
        {
            var pacote = _repositorio.BuscarPorId(query.IdPacote);

            if (pacote == null)
                return new GenericCommandResult(false, "Pacote não encontrado", null);

            var retorno = new BuscarPacotePorIdQueryResult()
            {
                Id = pacote.Id,
                Titulo = pacote.Titulo,
                Descricao = pacote.Descricao,
                Ativo = pacote.Ativo,
                QuantidadeComentarios = pacote.Comentarios.Count,
                Comentarios = (query.TipoUsuario == EnTipoUsuario.Admin ? GerarResultadoComentarios(pacote.Comentarios.ToList()) : GerarResultadoComentarios(pacote.Comentarios.Where(x => x.Status == EnStatusComentario.Publicado).ToList()))
            };

            return new GenericCommandResult(true, "Dados do pacote", retorno);
        }

        private List<ComentarioResult> GerarResultadoComentarios(List<Comentario> comentarios)
        {
            return comentarios.Select(c =>
            {
                return new ComentarioResult()
                {
                    Id = c.Id,
                    Texto = c.Texto,
                    Sentimento = c.Sentimento,
                    Status = c.Status.ToString(),
                    IdUsuario = c.IdUsuario,
                    IdPacote = c.IdPacote
                };
            }).ToList();
        }

        IQueryResult IHandlerQuery<BuscarPacotePorIdQuery>.Handle(BuscarPacotePorIdQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}