using CodeTur.Comum.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Codetur.Dominio.Commands.Pacotes
{
    public class AlterarPacoteCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
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
