using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Util;
using CodeTur.Dominio.Commands.Usuario;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Usuario
{
    public class AlterarSenhaCommandHandler : Notifiable, IHandlerCommand<AlterarSenhaCommand>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public AlterarSenhaCommandHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AlterarSenhaCommand command)
        {
            // Faz As Validações no Sistema
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Senha inválida", command.Notifications);

            var usuarioexiste = _repositorio.BuscarPorId(command.IdUsuario);

            if (usuarioexiste == null)
                return new GenericCommandResult(false, "Usuário não encontrado", command.Notifications);

            // Criptografa a Senha

            command.Senha = Senha.CriptografarSenha(command.Senha);
            usuarioexiste.AlterarSenha(command.Senha);

            _repositorio.Alterar(usuarioexiste);


            return new GenericCommandResult(true, "Senha Alterada", null);
        }
    }
}