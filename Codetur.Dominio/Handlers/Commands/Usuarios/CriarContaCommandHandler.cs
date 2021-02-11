using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Comum.Util;
using Flunt.Notifications;
using CodeTur.Dominio.Entidades;
using Codetur.Dominio.Entidades;

namespace CodeTur.Dominio.Handlers.Usuario
{
    public class CriarContaCommandHandler : Notifiable, IHandlerCommand<CriarContaCommand>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public CriarContaCommandHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(CriarContaCommand command)
        {
            //Faz as Validações 

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Verificação de Emails já existentes

            var usuarioExiste = _repositorio.BuscarPorEmail(command.Email);

            if (usuarioExiste != null)
                return new GenericCommandResult(false, "Email já cadastrado", null);


            //Produz a entidade Usuario

            var usuario = new Codetur.Dominio.Entidades.Usuario(command.Nome, command.Email, command.Senha, command.TipoUsuario);


            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuario.Notifications);

            if (!string.IsNullOrEmpty(command.Telefone))
                usuario.AdicionarTelefone(command.Telefone);

            //Criptografa a Senha

            usuario.AlterarSenha(Senha.CriptografarSenha(command.Senha));

            if (usuario.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", usuario.Notifications);

            //Salva o novo Usuario no Banco

            _repositorio.Adicionar(usuario);

            //Email de Boas Vindas

            return new GenericCommandResult(true, "Conta criada com Sucesso", null);
        }
    }
}
