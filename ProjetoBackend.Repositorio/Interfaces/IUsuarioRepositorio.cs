using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<int> AdicionarUsuario(Usuario usuario);
        Task AualizarUsuario(Usuario usuario);
        Task DeletarUsuario(int usuarioId);
        Task<Usuario> ObterPorID(int usuarioId);

        Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId);



    }
}
