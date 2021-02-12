using Flunt.Validations;
using CodeTur.Comum.ValoresObjeto;
using CodeTur.Comum.Enum;

namespace CodeTur.Dominio.ValueObjects
{
    public class Telefone : ValorObjeto
    {
        public string Numero { get; private set; }
        public EnTipoTelefone TipoTelefone { get; private set; }

        public Telefone(string numero, EnTipoTelefone tipoTelefone)
        {
            Numero = numero;
            TipoTelefone = tipoTelefone;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validar(), "Telefone.Numero", "Telefone Inválido")
                );
        }

        private bool Validar()
        {
            if (TipoTelefone == EnTipoTelefone.Celular && Numero.Length == 11)
                return true;

            if ((TipoTelefone == EnTipoTelefone.Comercial || TipoTelefone == EnTipoTelefone.Residencial) && Numero.Length == 10)
                return true;

            return false;
        }
    }
}
