using System;
using System.Collections.Generic;
using System.Text;

namespace DoceGrao.Api.Domain.Models.User
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(UserModel user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email.Address;
            Username = user.Credential.Login;
            Token = token;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
