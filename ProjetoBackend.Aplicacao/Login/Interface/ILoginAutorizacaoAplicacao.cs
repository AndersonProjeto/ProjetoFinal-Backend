using ProjetoBackend.Aplicacao.Login.DTO;

namespace ProjetoBackend.Aplicacao.Login.Interface
{
    public interface ILoginAutorizacaoAplicacao
    {
        Task<LoginRespostaDTO> Login(LoginDTO loginDTO);
    }
}
