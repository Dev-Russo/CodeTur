using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Commands.Pacote;
using Flunt.Notifications;

namespace CodeTur.Dominio.Handlers.Pacote
{
    public class AdicionarPacoteHandler : Notifiable, IHandlerCommand<AdicionarPacoteCommand>
    {
        private readonly IPacoteRepositorio _repositorio;

        public AdicionarPacoteHandler(IPacoteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ICommandResult Handle(AdicionarPacoteCommand command)
        {
            //Faz os Testes de Validação
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Faz a Verificação dos Titulos Para Evitar que eles se repitam
            var pacoteExiste = _repositorio.BuscarPorTitulo(command.Titulo);

            if (pacoteExiste != null)
                return new GenericCommandResult(false, "Pacote já cadastrado", null);

            //Gera uma entidade usuario
            var pacote = new Entidades.Pacote(command.Titulo, command.Descricao, command.Imagem, command.Ativo);

            if (pacote.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", pacote.Notifications);

            _repositorio.Adicionar(pacote);

            return new GenericCommandResult(true, "Pacote criado", null);
        }
    }
}