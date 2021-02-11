using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Commands.Pacote;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Pacote
{
    public class AlterarImagemHandler : Notifiable, IHandlerCommand<AlterarImagemPacoteCommand>
    {

        private readonly IPacoteRepositorio _repositorio;

        public AlterarImagemHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AlterarImagemPacoteCommand command)
        {
            // Faz os Testes de Validação
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Faz a Verificação do email para evitar repetidos
            var pacote = _repositorio.BuscarPorId(command.IdPacote);

            if (pacote == null)
                return new GenericCommandResult(false, "Pacote não encontrado", null);

            pacote.AtualizaImagem(command.Imagem);

            if (pacote.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", pacote.Notifications);

            //Faz o Salvamento no banco
            _repositorio.Alterar(pacote);

            //Envia o email de Boas-Vindas ao Usuario do Sistema

            return new GenericCommandResult(true, "Pacote alterado", null);
        }
    }
}