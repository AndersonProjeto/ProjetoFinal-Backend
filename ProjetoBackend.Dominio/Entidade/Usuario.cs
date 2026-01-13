using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Dominio.Entidade
{
    public class Usuario
    {
        public int UsuarioId { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;
        public DateTime DataNascimento { get; private set; }
        public decimal AlturaCm { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public ICollection<Treino> Treinos { get; private set; } = new List<Treino>();
        public ICollection<Evolucao> Evolucoes { get; private set; } = new List<Evolucao>();
        public ICollection<IAInteracao> IAInteracoes { get; private set; } = new List<IAInteracao>();

        protected Usuario() { }

        public Usuario(string nome, string email, string senhaHash, DateTime dataNascimento, decimal alturaCm)
        {
            AtualizarNome(nome);
            AtualizarEmail(email);
            AtualizarAltura(alturaCm);

            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha inválida");


            SenhaHash = senhaHash;
            DataNascimento = dataNascimento;
            DataCriacao = DateTime.UtcNow;

            Treinos = new List<Treino>();
            Evolucoes = new List<Evolucao>();
            IAInteracoes = new List<IAInteracao>();
        }

        public void AtualizarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório");

            Nome = nome;
        }

        public void AtualizarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório");

            Email = email;
        }

        public void AtualizarAltura(decimal alturaCm)
        {
            if (alturaCm <= 0)
                throw new ArgumentException("Altura inválida");

            AlturaCm = alturaCm;
        }

        public void AtualizarSenha(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha inválida");

            SenhaHash = senhaHash;
        }
    }
}
