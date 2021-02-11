using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Codetur.Dominio.Commands.Usuarios
{
    public class ResetarSenhaCommand : Notifiable, ICommand
    {
        public ResetarSenhaCommand()
        {

        }

        public ResetarSenhaCommand(string email)
        {
            Email = email;
        }

        public string Email { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsEmail(Email, "Email", "Informe um e-mail válido")
           );
        }
    }
}