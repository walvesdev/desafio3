using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desafio3A.Models
{
    public class ListaCompraVM
    {
        public int? Id { get; set; }
        public int IdAlimento { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}