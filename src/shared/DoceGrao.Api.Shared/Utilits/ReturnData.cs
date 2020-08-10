using System;
using System.Collections.Generic;
using System.Text;

namespace DoceGrao.Api.Shared.Utilits
{
    public class ReturnData<T>
    {
        public ReturnData(bool sucesso, List<string> message, T data)
        {
            Sucesso = sucesso;
            Message = message;
            Data = data;
        }

        public bool Sucesso { get; }
        public List<string> Message { get; }
        public T Data { get; }
    }
}
