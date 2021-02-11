using Codetur.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Codetur.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        void Adicionar(Usuario usuario);
        void Alterar(Usuario usuario);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorId(Guid id);
        IEnumerable<Usuario> Listar(bool? Ativo = null);


    }
}
