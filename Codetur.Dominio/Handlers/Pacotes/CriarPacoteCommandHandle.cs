using Codetur.Dominio.Commands.Pacotes;
using Codetur.Dominio.Repositorios;
using CodeTur.Comum.Commands;
using CodeTur.Comum.Handlers.Contracts;
using CodeTur.Dominio.Entidades;
using System;

namespace Codetur.Dominio.Handlers.Pacotes
{
    class CriarPacoteCommandHandle : IHandlerCommand<CriarPacoteCommand>
    {
        private readonly IPacoteRepositorio _pacoteRepositorio;

        public CriarPacoteCommandHandle(IPacoteRepositorio pacoteRepositorio)
        {
            _pacoteRepositorio = pacoteRepositorio;
        }
        public ICommandResult Handle(CriarPacoteCommand command)
        {
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(true, "Dados Inválidos", command.Notifications);

            var pacoteexiste = _pacoteRepositorio.BuscarPorTitulo(command.Titulo);

            if (pacoteexiste != null)
                return new GenericCommandResult(true, "Título do pacote já cadastrado", null);

            var pacote = new Pacote(command.Titulo, command.Descricao, command.Imagem, command.Ativo);

            if (pacote.Invalid)
                return new GenericCommandResult(true, "Dados inválidos", pacote.Notifications);

            _pacoteRepositorio.Adicionar(pacote);

            return new GenericCommandResult(true, "Pacote Criado", pacote);
        }
    }
}
