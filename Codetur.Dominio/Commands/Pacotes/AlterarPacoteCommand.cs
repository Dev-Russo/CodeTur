using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CodeTur.Dominio.Commands.Pacote
{
    public class AlterarPacoteCommand : Notifiable, ICommand
    {
        public AlterarPacoteCommand()
        {

        }

        public AlterarPacoteCommand(Guid idPacote, string titulo, string descricao)
        {
            IdPacote = idPacote;
            Titulo = titulo;
            Descricao = descricao;
        }

        public Guid IdPacote { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
               .Requires()
               .AreNotEquals(IdPacote, Guid.Empty, "IdPacote", "Id do pacote inválido")
               .IsNotNullOrEmpty(Titulo, "Titulo", "Informe o Título do pacote")
               .IsNotNullOrEmpty(Descricao, "Descricao", "Informe a Descrição do pacote")
           );
        }
    }
}
