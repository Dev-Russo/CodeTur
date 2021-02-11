using CodeTur.Comum.Enum;
using CodeTur.Comum.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace CodeTur.Dominio.Queries.Pacote
{
    public class BuscarPacotePorIdQuery : Notifiable, IQuery
    {
        public BuscarPacotePorIdQuery()
        { }

        public BuscarPacotePorIdQuery(Guid idPacote, EnTipoUsuario tipoUsuario)
        {
            IdPacote = idPacote;
            TipoUsuario = tipoUsuario;
        }

        public Guid IdPacote { get; set; }
        public EnTipoUsuario TipoUsuario { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(IdPacote, Guid.Empty, "IdPacote", "Informe o Id do Pacote")
            );
        }
    }

    public class BuscarPacotePorIdQueryResult
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        //Define o tipo como IReadOnlyCollection para definir como somente leitura, usuário não poderá mexer no objeto
        public int QuantidadeComentarios { get; set; }
        public ICollection<ComentarioResult> Comentarios { get; set; }
    }

    public class ComentarioResult
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public string Sentimento { get; set; }
        public string Status { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdPacote { get; set; }
    }
}