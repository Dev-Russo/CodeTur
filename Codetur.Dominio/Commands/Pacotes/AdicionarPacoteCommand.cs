using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CodeTur.Dominio.Commands.Pacote
{
    public class AdicionarPacoteCommand : Notifiable, ICommand
    {
        public AdicionarPacoteCommand()
        {

        }

        public AdicionarPacoteCommand(string titulo, string descricao, string imagem, bool ativo)
        {
            Titulo = titulo;
            Descricao = descricao;
            Imagem = imagem;
            Ativo = ativo;
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public bool Ativo { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                 .Requires()
                 .IsNotNullOrEmpty(Titulo, "Titulo", "Informe o Título do pacote")
                 .IsNotNullOrEmpty(Descricao, "Descricao", "Informe o Descrição do pacote")
                 .IsNotNullOrEmpty(Imagem, "Imagem", "Informe o Imagem do pacote")
             );
        }
    }
}
