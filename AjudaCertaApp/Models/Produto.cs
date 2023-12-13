using AjudaCertaApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public DateTime Validade { get; set; }
        public TipoProdutoEnum TipoProduto { get; set; }
        public StatusItemEnum StatusItem { get; set; }
        public ItemDoacao? ItemDoacao { get; set; }
        public int ItemDoacaoId { get; set; }
    }
}
