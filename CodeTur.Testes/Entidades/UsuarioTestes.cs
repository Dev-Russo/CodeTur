using Codetur.Dominio.Entidades;
using Xunit;

namespace CodeTur.Testes.Entidades
{
    public class UsuarioTestes
    {
        [Fact]
        public void DeveRetornarErroSeUsuarioInvalido()
        {
            var usuario = new Usuario("","fenando.guerra@corujsdev.com.br","123456", Comum.Enum.EnTipoUsuario.Comum);
            Assert.True(usuario.Invalid, "Usuário é válido");
        }
        [Fact]
        public void DeveRetornarErroSeEmailInvalido()
        {
            var usuario = new Usuario("", "fenando.guerracorujsdev.com.br", "123456", Comum.Enum.EnTipoUsuario.Comum);
            Assert.True(usuario.Invalid, "Usuário é válido");
        }
        [Fact]
        public void DeveRetornarSucessoSeUsuarioValido()
        {
            var usuario = new Usuario("Fernando Henrique", "fenando.guerra@corujsdev.com.br", "123456", Comum.Enum.EnTipoUsuario.Comum);
            Assert.True(usuario.Valid, "Usuário é inválido");
        }
        [Fact]
        public void DeveRetornarSucessoSeTelefoneUsuarioValido()
        {
            var usuario = new Usuario("Fernando Henrique", "fenando.guerra@corujsdev.com.br", "123456", Comum.Enum.EnTipoUsuario.Comum);
            usuario.AdicionarTelefone("11945414430");
            Assert.True(usuario.Valid, "Número do telefone é Inválido");
        }
    }
}
