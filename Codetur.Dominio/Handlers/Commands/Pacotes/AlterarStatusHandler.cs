using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Commands.Pacote;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Pacote
{
    public class AlterarStatusHandler : Notifiable, IHandlerCommand<AlterarStatusCommand>
    {

        private readonly IPacoteRepositorio _repositorio;

        public AlterarStatusHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AlterarStatusCommand command)
        {
            // Faz as Validações no Sistema

            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Verificação de Email existente

            var pacote = _repositorio.BuscarPorId(command.IdPacote);

            if (pacote == null)
                return new GenericCommandResult(false, "Pacote não encontrado", null);

            pacote.AlterarStatus(command.Status);

            if (pacote.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", pacote.Notifications);

            //  Altera o banco com o novo estado do objeto

            _repositorio.Alterar(pacote);

            //Envia o email de Boa-Vindas ao usuario do Sistema

            return new GenericCommandResult(true, "Pacote alterado", null);
        }
    }
}
