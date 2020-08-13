using System.Drawing;
using System.Text.Json.Serialization;
using DoceGrao.Api.Shared.ValueObjects;
using FluentValidation;

namespace DoceGrao.Api.Domain.Models.ValueObjects
{
    public class Credential : ValueObject
    {
        protected Credential() { }

        public Credential(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; private set; }
        [JsonIgnore]
        public string Password { get; private set; }
    }

    public class CredentialValidator : AbstractValidator<Credential>
    {
        public CredentialValidator()
        {
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage("Login/Email requerido");
            RuleFor(c => c.Password).NotNull().NotEmpty().WithMessage("Senha requerido");
        }
    }
}
