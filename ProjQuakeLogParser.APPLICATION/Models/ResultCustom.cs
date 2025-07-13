using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjQuakeLogParser.APPLICATION.Models
{
    public class ResultCustom
    {       
        protected ResultCustom(bool ehSucesso, string mensagem)
        {
            EhSucesso = ehSucesso;
            Mensagem = mensagem;
        }

        public bool EhSucesso { get; private set; }
        public string Mensagem { get; set; }
    }
}
