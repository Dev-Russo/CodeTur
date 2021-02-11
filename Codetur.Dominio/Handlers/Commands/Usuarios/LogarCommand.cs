using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Util;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Usuario
{
    public class LogarCommandHandler : Notifiable, IHandlerCommand<LogarCommand>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public LogarCommandHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(LogarCommand command)
        {
            //Validações no Sistema

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Email ou senha Inválidos", command.Notifications);

            //Verifica a existencia do Email

            var usuarioexiste = _repositorio.BuscarPorEmail(command.Email);

            if (usuarioexiste == null)
                return new GenericCommandResult(false, "Email inválido", command.Notifications);

            //Validação da Senha

            if (!Senha.Validar(command.Senha, usuarioexiste.Senha))
                return new GenericCommandResult(false, "Senha inválida", command.Notifications);


            return new GenericCommandResult(true, "Usuário logado", usuarioexiste);
        }
    }
}
