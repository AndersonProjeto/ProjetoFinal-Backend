using System;
using System.Collections.Generic;

namespace ProjetoBackend.Dominio
{
    public class Usuario
    {
        public int UsuarioId { get; private set; }

        public string Nome { get; private set; }
        public string Email { get; private set; }

        // 🔐 Hash da senha (NUNCA senha em texto)
        public string SenhaHash { get; private set; }

        public DateTime DataNascimento { get; private set; }
        public decimal AlturaCm { get; private set; }
        public DateTime DataCriacao { get; private set; }

        // 🔗 Relacionamentos
        public ICollection<Treino> Treinos { get; private set; }
        public ICollection<Evolucao> Evolucoes { get; private set; }
        public ICollection<IAInteracao> IAInteracoes { get; private set; }

        protected Usuario() { }

        public Usuario(
            string nome,
            string email,
            string senhaHash,
            DateTime dataNascimento,
            decimal alturaCm)
        {
            
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("Senha inválida");

            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            DataNascimento = dataNascimento;
            AlturaCm = alturaCm;
            DataCriacao = DateTime.UtcNow;

            Treinos = new List<Treino>();
            Evolucoes = new List<Evolucao>();
            IAInteracoes = new List<IAInteracao>();
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Nome inválido");

            Nome = novoNome;
        }

        public void AtualizarAltura(decimal novaAltura)
        {
            if (novaAltura <= 0)
                throw new ArgumentException("Altura inválida");

            AlturaCm = novaAltura;
        }
        public void AtualizarSenha(string novaSenhaHash)
        {
            if (string.IsNullOrWhiteSpace(novaSenhaHash))
                throw new ArgumentException("Senha inválida");

            SenhaHash = novaSenhaHash;
        }
    }
}
