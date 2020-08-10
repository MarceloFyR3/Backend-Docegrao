using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DoceGrao.Api.Domain.Models.Enum;
using DoceGrao.Api.Shared.Utilits;
using FluentValidation;

namespace DoceGrao.Api.Domain.ViewModels.User
{
    public class UserCreateViewModel
    {
        public Guid? Id { get; set; }
        public int? IdGrupo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public EDocumentType TypeDocument { get; set; }
    }

    public class UserCreateViewModelValidator : AbstractValidator<UserCreateViewModel>
    {
        public UserCreateViewModelValidator()
        {
            RuleFor(u => u.IdGrupo).NotNull().WithMessage("Grupo requerido");
            RuleFor(u => u.Email).EmailAddress().WithMessage("E-mail inválido");
            RuleFor(u => u.Name).Length(3, 100).WithMessage("Nome deve possuir minímo 3 e máximo de 30 caracteres");
            RuleFor(u => u.Login).Length(5, 30).WithMessage("Login deve possuir minímo 5 e máximo de 30 caracteres");
            RuleFor(u => u.Password).Must(PasswordRequirements)
                .WithMessage("Senha deve conter no mínimo 8 caracteres sendo pelo menos um número e uma letra");
            RuleFor(u => u.Document).NotNull().WithMessage("Documento requerido");
        }

        public bool PasswordRequirements(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            if (password.Length < 8 || !UtilBase.HasDigit(password) || !UtilBase.HasLetter(password)) return false;

            return true;
        }

    }
}
