using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjQuakeLogParser.APPLICATION.Models
{
    public class ResultCustomData<T> : ResultCustom
    {
        public ResultCustomData(bool ehSucesso, string mensagem, T data) : base(ehSucesso, mensagem)
        {
            this.data = data;
        }

        public T? data { get; set; }

        public static ResultCustomData<T> Sucesso(T? data) => new(true, string.Empty, data);

        public static ResultCustomData<T> Falha(string mensagem) => new(false, mensagem, default(T));
    }
}
