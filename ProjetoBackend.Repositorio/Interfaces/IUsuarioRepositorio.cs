using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<int> AdicionarUsuario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Task DeletarUsuario(int usuarioId);
        Task<Usuario?> ObterPorID(int usuarioId);
        Task<Usuario?> ObterPorEmail(string email);

        Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId);
        Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId);
        Task<UsuarioDetalhesDTO?> ObterUsuarioDetalhes(int usuarioId);


    }
}
