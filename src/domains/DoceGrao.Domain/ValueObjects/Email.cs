using System;
using System.Collections.Generic;
using System.Text;
using DoceGrao.Api.Shared.ValueObjects;
using Flunt.Validations;

namespace DoceGrao.Api.Domain.Models.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

        }

        public string Address { get; private set; }

    }
}
