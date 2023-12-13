using AjudaCertaApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models.ListagemDoacao
{
    public class ListaDoacao
    {
        public int Id { get; set; }
        public DateTime DataDoacao { get; set; }
        public DateTime DataAgenda { get; set; }
        public double? Dinheiro { get; set; }
        public string? ItemDoacaoNome { get; set; }
        public string? ItemDoacaoDescricao { get; set; }
        public TipoItemEnum? TipoItem { get; set; }
        public int? Quantidade { get; set; }
        public StatusDoacaoEnum StatusDoacao { get; set; }
        public string? Tamanho { get; set; }
        public string? CondicaoM { get; set; }
        public string? CondicaoR { get; set; }
        public string? MedidaM { get; set; }
        public string? CondicaoE { get; set; }
        public string? MedidaE { get; set; }
        public string? FaixaEtaria { get; set; }
        public string? Genero { get; set; }
        public DateTime? Validade { get; set; }
        public TipoProdutoEnum? TipoProduto {get; set;}
        public bool roupa { get; set; }
        public bool dinheiro { get; set; }
        public bool mobilia { get; set; }
        public bool eletrodomestico { get; set; }
        public bool produto { get; set; }
        public bool quantidade { get; set; }
        public bool descricao { get; set; }
        public bool pendente { get; set; }
        public bool cancelada { get; set; }
        public bool concluida { get; set; }

    }
}
