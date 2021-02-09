using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Codetur.Dominio.Commands.Usuarios
{
    class ResetarSenhaCommand : Notifiable, ICommand
    {
        public string Email { get; private set; }
        public void Validar()
        {
            {
                AddNotifications(new Contract()
                  .IsEmail(Email, "Email", "Informe um e-mail válido+")
                );
            }
        }
    }
}