using CodeTur.Comum.Commands;
using CodeTur.Comum.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CodeTur.Dominio.Commands.Comentario
{
    public class AlterarComentarioCommand : Notifiable, ICommand
    {
        public AlterarComentarioCommand(Guid idPacote, string texto, string sentimento, EnStatusComentario status)
        {
            IdPacote = idPacote;
            Texto = texto;
            Sentimento = sentimento;
            Status = status;
        }

        public Guid IdPacote { get; set; }
        public string Texto { get; private set; }
        public string Sentimento { get; private set; }
        public EnStatusComentario Status { get; private set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Texto, "Texto", "Informe o Texto do comentário")
                .IsNotNullOrEmpty(Sentimento, "Sentimento", "Informe o sentimento do comentário")
                .AreNotEquals(IdPacote, Guid.Empty, "IdUsuario", "Informe o Id do Usuário do comentário")
            );
        }
    }
}
