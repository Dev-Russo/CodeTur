using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Commands.Pacote;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Pacote
{
    public class AlterarPacoteHandler : Notifiable, IHandlerCommand<AlterarPacoteCommand>
    {
        private readonly IPacoteRepositorio _repositorio;

        public AlterarPacoteHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AlterarPacoteCommand command)
        {
            // Faz as Validações do Sistema

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Faz a Verificação para evitar que email se repitam

            var pacote = _repositorio.BuscarPorId(command.IdPacote);

            if (pacote == null)
                return new GenericCommandResult(false, "Pacote não encontrado", null);

            //Faz a Verificação do Pacote pelo mesmo motivo do email

            var pacoteExiste = _repositorio.BuscarPorTitulo(command.Titulo);

            if (pacoteExiste != null)
                return new GenericCommandResult(false, "Pacote já cadastrado", null);

            //Faz As alterações nescessarias no Titulo e Descrição do Pacote

            pacote.AtualizaPacote(command.Titulo, command.Descricao);

            if (pacote.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", pacote.Notifications);

            //Salva o Ususario do Sistema no Banco de Dados

            _repositorio.Alterar(pacote);

            //Enviar email de boas vindas

            return new GenericCommandResult(true, "Pacote alterado", null);
        }
    }
}