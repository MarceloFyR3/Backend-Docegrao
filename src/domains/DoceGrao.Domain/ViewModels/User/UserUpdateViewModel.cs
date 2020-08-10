using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace DoceGrao.Api.Domain.ViewModels.User
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }
        public int? IdGrupo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserUpdateViewModelValidator : AbstractValidator<UserUpdateViewModel>
    {
        public UserUpdateViewModelValidator()
        {
            RuleFor(u => u.Id).NotNull().NotEmpty().WithMessage("Usuário inválido");
            RuleFor(u => u.IdGrupo).NotEmpty().NotNull().WithMessage("Grupo de usuário requerido");
            RuleFor(u => u.Name).NotNull().NotEmpty().WithMessage("Nome do usuário requerido");
            RuleFor(u => u.Email).NotNull().NotEmpty().WithMessage("E-mail do usuário requerido");
        }
    }
}
