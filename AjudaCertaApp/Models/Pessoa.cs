
using AjudaCertaApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public string Genero { get; set; }
        public FisicaJuridicaEnum fisicaJuridica { get; set; }
        public TipoPessoaEnum Tipo { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId {  get; set; }
        public Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
    }
}
