using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Util;
using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Repositorios;
using Flunt.Notifications;
using Codetur.Dominio.Entidades;

namespace Codetur.Dominio.Handlers.Usuarios
{
    public class CriarContaHandle : Notifiable, IHandlerCommand<CriarContaCommand>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public CriarContaHandle(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public ICommandResult Handle(CriarContaCommand command)
        {
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados Inválidos", command.Notifications);

            // Verifica se email existe
            var usuarioExiste = _usuarioRepositorio.BuscarPorEmail(command.Email);

            if (usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado, informe outro email", null);

            // Criptografar senha
            command.Senha = Senha.CriptografarSenha(command.Senha);

            // Salvar no banco
            var usuario = new Usuario(command.Nome, command.Email, command.Senha, command.TipoUsuario);
            //Verifica se foi passado o telefone, caso sim inclui o mesmo
            if (!string.IsNullOrEmpty(command.Telefone))
                usuario.AdicionarTelefone(command.Telefone);

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados do usuário inválidos", usuario.Notifications);

            _usuarioRepositorio.Adicionar(usuario);

            // Enviar Email de boas Vindas para o meu usuário
             

            return new GenericCommandResult(true, "Usuário Criado", usuario);
        }
    }
}
