using Codetur.Dominio.Commands.Usuarios;
using Codetur.Dominio.Handlers.Usuarios;
using CodeTur.Comum.Commands;
using CodeTur.Testes.Repositorios;
using Xunit;

namespace CodeTur.Testes.Handlers.Usuarios
{
    public class CriarUsuarioHandleTestes
    {
        [Fact]
        public void DeveRetornarErroCasoOsDadosDoCommandSejamInvalidos()
        {
            var command = new CriarContaCommand();

            var handle = new CriarContaHandle(new FakeUsuarioRepositorio());

            var resultado = (GenericCommandResult)handle.Handle(command);

            Assert.False(resultado.Sucesso, "Usuário válido");
        }
        [Fact]
        public void DeveRetornarErroCasoOsDadosDoCommandSejamValidos()
        {
            var command = new CriarContaCommand("Fernando","email3@email.com","1234567","", Comum.Enum.EnTipoUsuario.Comum );

            var handle = new CriarContaHandle(new FakeUsuarioRepositorio());

            var resultado = (GenericCommandResult)handle.Handle(command);

            Assert.True(resultado.Sucesso, "Usuário válido");
        }
    }
}
