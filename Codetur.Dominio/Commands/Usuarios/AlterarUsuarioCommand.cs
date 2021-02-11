using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CodeTur.Dominio.Commands.Usuario
{
    public class AlterarUsuarioCommand : Notifiable, ICommand
    {
        public AlterarUsuarioCommand()
        {

        }

        public AlterarUsuarioCommand(Guid idUsuario, string nome, string email, string telefone)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Nome, 3, "Nome", "Nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(Nome, 40, "Nome", "Nome deve conter até 40 caracteres")
                .IsEmail(Email, "Email", "Informe um e-mail válido")
            );
        }
    }
}
