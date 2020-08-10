using System;
using DoceGrao.Api.Domain.Models.Enum;
using DoceGrao.Api.Shared.ValueObjects;
using Flunt.Validations;

namespace DoceGrao.Api.Domain.Models.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, DateTime dateIssue, EDocumentType type)
        {
            Number = number;
            DateIssue = dateIssue;
            Type = type;
        }

        public string Number { get; private set; }
        public DateTime DateIssue { get;  private set; }
        public EDocumentType Type { get; private set; }

        private bool Validate()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
                return true;

            if (Type == EDocumentType.CPF && Number.Length == 11)
            {
                while (Number.Length != 11)
                    Number = '0' + Number;

                var igual = true;
                for (var i = 1; i < 11 && igual; i++)
                    if (Number[i] != Number[0])
                        igual = false;

                if (igual || Number == "12345678909")
                    return false;
                
                var numeros = new int[11];

                for (var i = 0; i < 11; i++)
                    numeros[i] = int.Parse(Number[i].ToString());

                var soma = 0;
                for (var i = 0; i < 9; i++)
                    soma += (10 - i) * numeros[i];

                var resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                        return false;
                }
                else if (numeros[9] != 11 - resultado)
                    return false;

                soma = 0;
                for (var i = 0; i < 10; i++)
                    soma += (11 - i) * numeros[i];

                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                        return false;
                }
                else if (numeros[10] != 11 - resultado)
                    return false;

                return true;
            }
                

            return false;
        }
    }
}
