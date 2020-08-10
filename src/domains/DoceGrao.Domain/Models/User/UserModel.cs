using System;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Shared.Entities;

namespace DoceGrao.Api.Domain.Models.User
{
    public class UserModel : Entity
    {
        // Para o EF
        public UserModel(byte[] saltKey)
        {
            SaltKey = saltKey;
        }

        public UserModel(Guid id, int idGrupo, string name, Email email, Credential credential, byte[] saltKey, Document document, bool active, bool block, DateTime? dateBlock, int? incorrectAttempts, DateTime? dateRegister, bool emailConfirmed, string urlImg)
        {
            Id = id;
            IdGrupo = idGrupo;
            Name = name;
            Email = email;
            Credential = credential;
            SaltKey = saltKey;
            Document = document;
            Active = active;
            Block = block;
            DateBlock = dateBlock;
            IncorrectAttempts = incorrectAttempts;
            DateRegister = dateRegister;
            EmailConfirmed = emailConfirmed;
            UrlImg = urlImg;
        }

        public int IdGrupo { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public Credential Credential { get; set; }
        public byte[] SaltKey { get; private set; }
        public Document Document { get; set; }
        public bool Active { get; set; }
        public bool Block { get; set; }
        public DateTime? DateBlock { get; set; }
        public int? IncorrectAttempts { get; set; }
        public DateTime? DateRegister { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UrlImg { get; set; }

    }
}
