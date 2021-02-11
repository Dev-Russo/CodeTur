using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CodeTur.Dominio.Commands.Usuario
{
    public class AlterarSenhaCommand : Notifiable, ICommand
    {
        public AlterarSenhaCommand()
        {

        }

        public AlterarSenhaCommand(Guid idUsuario, string email)
        {
            IdUsuario = idUsuario;
            Senha = email;
        }

        public Guid IdUsuario { get; set; }
        public string Senha { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .AreNotEquals(IdUsuario, Guid.Empty, "IdUsuario", "Id do usuário inválido")
                .HasMinLen(Senha, 6, "Senha", "Senha deve ter no minímo 6 caracteres")
            );
        }
    }
}
