using Flunt.Notifications;
using System;

namespace CodeTur.Comum.Entidades
{
    public abstract class Entidade : Notifiable
    {

        public Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            DataAlteracao = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
