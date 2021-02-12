using CodeTur.Comum.ValoresObjeto;
using Flunt.Validations;

namespace CodeTur.Dominio.ValueObjects
{
    public class Email : ValorObjeto
    {
        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Endereco, "Email.Address", "Email inválido"));
        }

        public string Endereco { get; private set; }
    }
}

