using AjudaCertaApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models
{
    public class Eletrodomestico
    {
        public int Id { get; set; }
        public string Medida { get; set; }
        public string Condicao { get; set; }
        public StatusItemEnum StatusItem { get; set; }
        public ItemDoacao? ItemDoacao { get; set; }
        public int ItemDoacaoId { get; set; }
    }
}
