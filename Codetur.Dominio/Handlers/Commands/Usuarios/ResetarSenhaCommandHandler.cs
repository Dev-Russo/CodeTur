using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Util;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Usuario
{
    public class ResetarSenhaCommandHandler : Notifiable, IHandlerCommand<ResetarSenhaCommand>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public ResetarSenhaCommandHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(ResetarSenhaCommand command)
        {
            //Validações no Sistema

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Verificação do Email

            var usuario = _repositorio.BuscarPorEmail(command.Email);

            if (usuario == null)
                return new GenericCommandResult(false, "Email inválido", null);

            //Gera a nova senha para o acesso do Usuario

            string senha = Senha.Gerar();

            //Criptografa a Senha

            usuario.AlterarSenha(Senha.CriptografarSenha(senha));

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuario.Notifications);

            //Salvar usuario banco

            _repositorio.Alterar(usuario);

            //Envia o email mostrando a nova senha do Usuario

            return new GenericCommandResult(true, "Uma nova senha foi criada e enviada para o seu e-mail, verifique!!!", null);
        }
    }
}
