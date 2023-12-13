using AjudaCertaApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public StatusDoacaoEnum Status { get; set; }
        public Pessoa? Pessoa { get; set; }
        public int PessoaId { get; set; }
        public Endereco? Endereco { get; set; }
        public int EnderecoId { get; set; }
        public Doacao? Doacao { get; set; }
    }
}
