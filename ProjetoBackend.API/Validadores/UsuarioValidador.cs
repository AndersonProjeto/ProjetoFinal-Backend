using FluentValidation;
using ProjetoBackend.Aplicacao.DTOs.Usuario;

namespace ProjetoBackend.API.Validadores
{
    public class UsuarioValidador : AbstractValidator<AdicionarUsuarioDTO>
    {
        public UsuarioValidador()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.")
                .MaximumLength(100).WithMessage("Email deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]").WithMessage("Senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]").WithMessage("Senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]").WithMessage("Senha deve conter pelo menos um número.");

            RuleFor(x => x.DataNascimento)
                 .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                 .LessThan(DateTime.Now.Date)
                 .WithMessage("A data de nascimento não pode estar no futuro.")
                 .GreaterThan(new DateTime(1900, 1, 1))
                 .WithMessage("A data de nascimento informada é inválida.");
        }
    }
}