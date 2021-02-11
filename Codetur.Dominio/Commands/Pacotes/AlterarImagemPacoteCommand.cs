using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CodeTur.Dominio.Commands.Pacote
{
    public class AlterarImagemPacoteCommand : Notifiable, ICommand
    {
        public AlterarImagemPacoteCommand()
        {

        }

        public AlterarImagemPacoteCommand(Guid idPacote, string imagem)
        {
            IdPacote = idPacote;
            Imagem = imagem;
        }

        public Guid IdPacote { get; set; }
        public string Imagem { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
               .Requires()
               .AreNotEquals(IdPacote, Guid.Empty, "IdUsuario", "Id do usuário inválido")
               .IsNotNullOrEmpty(Imagem, "Imagem", "Informe a imagem do pacote")
           );
        }
    }
}
