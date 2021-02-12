using CodeTur.Comum.ValoresObjeto;
using Flunt.Validations;

namespace CodeTur.Dominio.ValueObjects
{
    public class Senha : ValorObjeto
    {
        public Senha(string senha)
        {
            Text = senha;

            AddNotifications(new Contract()
                .Requires()
                .HasMaxLen(Text, 16, "Senha.Text", "Senha deve ter no máximo 16 caracteres")
                .HasMinLen(Text, 6, "Senha.Text", "Senha deve ter no minimo 6 caracteres")
            );
        }

        public string Text { get; set; }
    }
}
