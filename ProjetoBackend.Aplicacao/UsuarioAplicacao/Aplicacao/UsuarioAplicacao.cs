
using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Aplicacao.Seguranca;
using ProjetoBackend.Aplicacao.Usuarios.Interfaces;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Dominio.DTOs.Usuario;
using ProjetoBackend.Repositorio.Interfaces;


namespace ProjetoBackend.Aplicacao.Usuarios.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISenhahashAplicacao _senhahashAplicacao;

        public UsuarioAplicacao(
            IUsuarioRepositorio usuarioRepositorio,
            ISenhahashAplicacao senhahashAplicacao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _senhahashAplicacao = senhahashAplicacao;
        }

        public async Task<int> AdicionarUsuario(AdicionarUsuarioDTO dto)
        {
            var senhaHash = _senhahashAplicacao.GerarHash(dto.Senha);

           var usuario = new Usuario
            (
                nome: dto.Nome,
                email: dto.Email,
                senhaHash: senhaHash,
                dataNascimento: dto.DataNascimento,
                alturaCm: dto.AlturaCm
            );

            return await _usuarioRepositorio.AdicionarUsuario(usuario);
        }

        public async Task AtualizarUsuario(AtualizarUsuarioDTO dto)
        {
            var usuario = await _usuarioRepositorio.ObterPorID(dto.UsuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.AtualizarNome(dto.Nome);
            usuario.AtualizarEmail(dto.Email);
            usuario.AtualizarAltura(dto.AlturaCm);

            await _usuarioRepositorio.AtualizarUsuario(usuario);
        }

        public async Task AlterarSenha(AlterarSenhaDTO dto)
        {
            var usuario = await _usuarioRepositorio.ObterPorID(dto.UsuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            var novaSenhaHash = _senhahashAplicacao.GerarHash(dto.NovaSenha);
            usuario.AtualizarSenha(novaSenhaHash);

            await _usuarioRepositorio.AtualizarUsuario(usuario);
        }

        public async Task DeletarUsuario(int usuarioId)
        {
            var usuario = await _usuarioRepositorio.ObterPorID(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            await _usuarioRepositorio.DeletarUsuario(usuarioId);
        }

        public async Task<UsuarioDetalhesDTO?> ObterId(int usuarioId)
        {
            if (usuarioId <= 0)
            {

            }
                throw new ArgumentException("ID inválido.");

            return await _usuarioRepositorio.ObterUsuarioDetalhes(usuarioId);
        }

        public async Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId)
        {
            return await _usuarioRepositorio.ObterUsuarioResumo(usuarioId);
        }

        public async Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId)
        {
            return await _usuarioRepositorio.ObterUltimaEvolucao(usuarioId);
        }
    }
}
