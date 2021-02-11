using CodeTur.Comum.Queries;
using System;

namespace CodeTur.Dominio.Queries.Usuario
{
    public class ListarUsuarioQuery : IQuery
    {
        public void Validar()
        {

        }
    }

    public class ListarQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        //Define o tipo como IReadOnlyCollection para definir como somente leitura, usuário não poderá mexer no objeto
        public int QuantidadeComentarios { get; set; }
    }
}
