using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Commands.Usuario;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Usuario
{
    public class AlterarUsuarioCommandHandler : Notifiable, IHandlerCommand<AlterarUsuarioCommand>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public AlterarUsuarioCommandHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AlterarUsuarioCommand command)
        {
            // Validações no Sitema

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Verificação de Email já existentes

            var usuario = _repositorio.BuscarPorId(command.IdUsuario);

            if (usuario == null)
                return new GenericCommandResult(false, "Usuário não encontrado", null);

            //Verifica se Outro Usuario já tem o mesmo e-mail cadastrado

            if (usuario.Email != command.Email)
            {
                var emailExiste = _repositorio.BuscarPorEmail(command.Email);

                if (emailExiste != null)
                    return new GenericCommandResult(false, "Email já cadastrado", null);
            }

            usuario.AlterarUsuario(command.Nome, command.Email);

            if (!string.IsNullOrEmpty(command.Telefone))
                usuario.AdicionarTelefone(command.Telefone);

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuario.Notifications);

            //Salva as Alterações do Usuario no bando

            _repositorio.Alterar(usuario);

            //Manda email de boas vindas

            return new GenericCommandResult(true, "Conta alterada com Sucesso", null);
        }
    }
}
