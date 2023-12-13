using AjudaCertaApp.Models;
using AjudaCertaApp.Models.ListagemDoacao;
using AjudaCertaApp.Models.Enuns;
using AjudaCertaApp.Services.Agendas;
using AjudaCertaApp.Services.Doacoes;
using AjudaCertaApp.Services.ItemDoacao;
using AjudaCertaApp.Services.ItemDoacaoDoado;
using AjudaCertaApp.Services.Pessoas;
using AjudaCertaApp.Services.Usuarios;
using AjudaCertaApp.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.Maui.DataSource.Extensions;
using AjudaCertaApp.Services.Enderecos;

namespace AjudaCertaApp.ViewModels.Perfil
{
    public class ListagemDoacoesViewModel : BaseViewModel
    {
        private DoacaoService dService;
        private UsuarioService uService;
        private PessoaService pService;
        private string _token;
        public ObservableCollection<Models.Doacao> Doacoes { get; set; }
        public ObservableCollection<Models.ListagemDoacao.ListaDoacao> ListaDoacaos { get; set; }
        public ListagemDoacoesViewModel()
        {
            _token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuarioService(_token);
            dService = new DoacaoService(_token);
            pService = new PessoaService(_token);
            Doacoes = new ObservableCollection<Models.Doacao>();

            _ = ObterDoacoes();
            CarregarUsuario();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AlterarFotoCommand = new Command(async () => AlterarFoto());
        }

        public ICommand AlterarFotoCommand { get; set; }

        #region AtributosPropriedades

        private bool roupa;
        public bool Roupa
        {
            get { return roupa; }
            set
            {
                roupa = value;
                OnPropertyChanged();
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string descricaoMarion;
        public string DescricaoMarion
        {
            get { return descricaoMarion; }
            set
            {
                descricaoMarion = value;
                OnPropertyChanged();
            }
        }

        private string ruaEtec;
        public string RuaEtec
        {
            get { return ruaEtec; }
            set
            {
                ruaEtec = value;
                OnPropertyChanged();
            }
        }

        private bool dinheiro;
        public bool Dinheiro
        {
            get { return dinheiro; }
            set
            {
                dinheiro = value;
                OnPropertyChanged();
            }
        }

        private bool mobilia;
        public bool Mobilia
        {
            get { return mobilia; }
            set
            {
                mobilia = value;
                OnPropertyChanged();
            }
        }

        private bool eletrodomestico;
        public bool Eletrodomestico
        {
            get { return eletrodomestico; }
            set 
            {
                eletrodomestico = value;
                OnPropertyChanged();
            }
        }

        private bool produto;
        public bool Produto
        {
            get { return produto; }
            set
            {
                produto = value;
                OnPropertyChanged();
            }
        }

        private bool quantidade;
        public bool Quantidade
        {
            get { return quantidade; }
            set
            {
                quantidade = value;
                OnPropertyChanged();
            }
        }

        private bool descricao;
        public bool Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fonteImagem;

        public ImageSource FonteImagem
        {
            get { return fonteImagem; }
            set
            {
                fonteImagem = value;
                OnPropertyChanged();
            }
        }

        private byte[] foto;

        public byte[] Foto
        {
            get => foto;
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Métodos
        public async Task ObterDoacoes()
        {
            try
            {
                Pessoa p = await pService.GetPessoaPorUsuarioAsync();

                if (p.Tipo == TipoPessoaEnum.ONG)
                {
                    Doacoes = await dService.GetDoacoesAsync();

                    AgendaService ags = new(_token);

                    foreach (var d in Doacoes)
                    {
                        d.Agenda = new();
                        d.Agenda = await ags.GetAgendaPorId(d.AgendaId);

                        if(d.Dinheiro != 0)
                        {
                            
                        }
                        else 
                        {
                            Dinheiro = false;

                            ItemDoacaoDoadoService idds = new(_token);
                            d.ItemDoacaoDoados = new();
                            d.ItemDoacaoDoados.Add(await idds.GetPorIdDoacao(d.Id));

                            d.ItemDoacaoDoados.First().ItemDoacao = new();
                            

                            ItemDoacaoService ids = new(_token);
                            d.ItemDoacaoDoados.First().ItemDoacao = await ids.GetPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                            if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.PRODUTO)
                            {
                                Produto produto = new();
                                produto = await ids.GetProdutoPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                                d.ItemDoacaoDoados.First().ItemDoacao.Produtos = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Produtos.Add(produto);
                                Produto = true;
                                Descricao = true;
                                Eletrodomestico = false;
                                Roupa = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.ROUPA)
                            {
                                Roupa roupa = new();
                                roupa = await ids.GetRoupaPorId(d.ItemDoacaoDoados.First().ItemDoacaoId); ;

                                d.ItemDoacaoDoados.First().ItemDoacao.Roupas = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Roupas.Add(roupa);
                                Descricao = true;
                                Roupa = true;
                                Produto = false;
                                Eletrodomestico = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.MOBILIA)
                            {
                                Mobilia mobilia = new();
                                mobilia = await ids.GetMobiliaPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                                d.ItemDoacaoDoados.First().ItemDoacao.Mobilias = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Mobilias.Add(mobilia);
                                Descricao = true;
                                Mobilia = true;
                                Produto = false;
                                Eletrodomestico = false;
                                Roupa = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.ELETRODOMESTICO)
                            {
                                Eletrodomestico e = new();
                                e = await ids.GetEletrodomesticoPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);
                                
                                d.ItemDoacaoDoados.First().ItemDoacao.Eletrodomesticos = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Eletrodomesticos.Add(e);

                                Descricao = true;
                                Eletrodomestico = true;
                                Produto = false;
                                Roupa = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                        }
                    }
                   OnPropertyChanged(nameof(Doacoes));

                    ListaDoacaos = new ObservableCollection<ListaDoacao>();
                    foreach(Models.Doacao d in Doacoes)
                    {
                        ListaDoacao l = new();
                        
                        l.Id = d.Id;
                        l.DataDoacao = d.Data;
                        l.DataAgenda = d.Agenda.Data;
                        l.StatusDoacao = d.StatusDoacao;
                        if(l.StatusDoacao == StatusDoacaoEnum.PENDENTE)
                        {
                            l.pendente = true; l.cancelada = false; l.concluida = false;
                        }
                        else if (l.StatusDoacao == StatusDoacaoEnum.CONCLUIDO)
                        {
                            l.pendente = false; l.cancelada = false; l.concluida = true;
                        }
                        else if (l.StatusDoacao == StatusDoacaoEnum.CANCELADO)
                        {
                            l.pendente = false; l.cancelada = true; l.concluida = false;
                        }

                        l.Dinheiro = d.Dinheiro;
                        if(l.Dinheiro == 0)
                        {
                            l.ItemDoacaoNome = d.ItemDoacaoDoados.First().ItemDoacao.Nome;
                            l.ItemDoacaoDescricao = d.ItemDoacaoDoados.First().ItemDoacao.Descricao;
                            l.Quantidade = d.ItemDoacaoDoados.First().ItemDoacao.Quantidade;
                            l.TipoItem = d.ItemDoacaoDoados.First().ItemDoacao.TipoItem;
                            
                            if(l.TipoItem == TipoItemEnum.PRODUTO)
                            {
                                l.Validade = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Produtos.First().Validade;
                                l.TipoProduto = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Produtos.First().TipoProduto;

                                if(l.TipoProduto == TipoProdutoEnum.CESTA_BASICA) 
                                {
                                    l.roupa = false;
                                    l.dinheiro = false;
                                    l.mobilia = false;
                                    l.eletrodomestico = false;
                                    l.quantidade = true;
                                    l.produto = false;
                                    l.descricao = false;
                                }
                                else 
                                {
                                    l.roupa = false;
                                    l.dinheiro = false;
                                    l.mobilia = false;
                                    l.eletrodomestico = false;
                                    l.quantidade = true;
                                    l.produto = true;
                                    l.descricao = true;
                                }
                            }
                            else if(l.TipoItem == TipoItemEnum.ROUPA)
                            {
                                if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().FaixaEtaria == FaixaEtariaEnum.ADULTO)
                                    l.FaixaEtaria = "Adulto";
                                else if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().FaixaEtaria == FaixaEtariaEnum.INFANTIL)
                                    l.FaixaEtaria = "Infantil";

                                l.Tamanho = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Tamanho;

                                if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Genero == GeneroEnum.MASCULINO)
                                    l.Genero = "Masculino";
                                else if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Genero == GeneroEnum.FEMININO)
                                    l.Genero = "Feminino";

                                l.CondicaoR = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Condicao;

                                l.roupa = true;
                                l.dinheiro = false;
                                l.mobilia = false;
                                l.eletrodomestico = false;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }
                            else if(l.TipoItem == TipoItemEnum.MOBILIA)
                            {
                                l.CondicaoM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Condicao;
                                l.MedidaM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Medida;
                                l.roupa = false;
                                l.dinheiro = false;
                                l.mobilia = true;
                                l.eletrodomestico = false;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }
                            else if (l.TipoItem == TipoItemEnum.ELETRODOMESTICO)
                            {
                                l.CondicaoE = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Eletrodomesticos.First().Condicao;
                                l.MedidaM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Medida;

                                l.roupa = false;
                                l.dinheiro = false;
                                l.mobilia = false;
                                l.eletrodomestico = true;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }

                        }
                        else
                        {
                            l.ItemDoacaoNome = "Doação monetária";

                            l.roupa = false;
                            l.dinheiro = true;
                            l.mobilia = false;
                            l.eletrodomestico = false;
                            l.quantidade = false;
                            l.produto = false;
                            l.descricao = false;
                        }

                        ListaDoacaos.Add(l);
                    }

                    ListaDoacaos = ListaDoacaos.OrderByDescending(x => x.DataDoacao).ToObservableCollection();
                    OnPropertyChanged(nameof(ListaDoacaos));
                }
                else if(p.Tipo == TipoPessoaEnum.DOADOR)
                {
                    Doacoes = await dService.GetDoacoesByPessoa();

                    AgendaService ags = new(_token);

                    foreach (var d in Doacoes)
                    {
                        d.Agenda = new();
                        d.Agenda = await ags.GetAgendaPorId(d.AgendaId);

                        if (d.Dinheiro != 0)
                        {

                        }
                        else
                        {
                            Dinheiro = false;

                            ItemDoacaoDoadoService idds = new(_token);
                            d.ItemDoacaoDoados = new();
                            d.ItemDoacaoDoados.Add(await idds.GetPorIdDoacao(d.Id));

                            d.ItemDoacaoDoados.First().ItemDoacao = new();


                            ItemDoacaoService ids = new(_token);
                            d.ItemDoacaoDoados.First().ItemDoacao = await ids.GetPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                            if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.PRODUTO)
                            {
                                Produto produto = new();
                                produto = await ids.GetProdutoPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                                d.ItemDoacaoDoados.First().ItemDoacao.Produtos = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Produtos.Add(produto);
                                Produto = true;
                                Descricao = true;
                                Eletrodomestico = false;
                                Roupa = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.ROUPA)
                            {
                                Roupa roupa = new();
                                roupa = await ids.GetRoupaPorId(d.ItemDoacaoDoados.First().ItemDoacaoId); ;

                                d.ItemDoacaoDoados.First().ItemDoacao.Roupas = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Roupas.Add(roupa);
                                Descricao = true;
                                Roupa = true;
                                Produto = false;
                                Eletrodomestico = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.MOBILIA)
                            {
                                Mobilia mobilia = new();
                                mobilia = await ids.GetMobiliaPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                                d.ItemDoacaoDoados.First().ItemDoacao.Mobilias = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Mobilias.Add(mobilia);
                                Descricao = true;
                                Mobilia = true;
                                Produto = false;
                                Eletrodomestico = false;
                                Roupa = false;
                                Quantidade = true;
                            }
                            else if (d.ItemDoacaoDoados.First().ItemDoacao.TipoItem == TipoItemEnum.ELETRODOMESTICO)
                            {
                                Eletrodomestico e = new();
                                e = await ids.GetEletrodomesticoPorId(d.ItemDoacaoDoados.First().ItemDoacaoId);

                                d.ItemDoacaoDoados.First().ItemDoacao.Eletrodomesticos = new();
                                d.ItemDoacaoDoados.First().ItemDoacao.Eletrodomesticos.Add(e);

                                Descricao = true;
                                Eletrodomestico = true;
                                Produto = false;
                                Roupa = false;
                                Mobilia = false;
                                Quantidade = true;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(Doacoes));

                    ListaDoacaos = new ObservableCollection<ListaDoacao>();
                    foreach (Models.Doacao d in Doacoes)
                    {
                        ListaDoacao l = new();

                        l.Id = d.Id;
                        l.DataDoacao = d.Data;
                        l.DataAgenda = d.Agenda.Data;
                        l.StatusDoacao = d.StatusDoacao;
                        if (l.StatusDoacao == StatusDoacaoEnum.PENDENTE)
                        {
                            l.pendente = true; l.cancelada = false; l.concluida = false;
                        }
                        else if (l.StatusDoacao == StatusDoacaoEnum.CONCLUIDO)
                        {
                            l.pendente = false; l.cancelada = false; l.concluida = true;
                        }
                        else if (l.StatusDoacao == StatusDoacaoEnum.CANCELADO)
                        {
                            l.pendente = false; l.cancelada = true; l.concluida = false;
                        }

                        l.Dinheiro = d.Dinheiro;
                        if (l.Dinheiro == 0)
                        {
                            l.ItemDoacaoNome = d.ItemDoacaoDoados.First().ItemDoacao.Nome;
                            l.ItemDoacaoDescricao = d.ItemDoacaoDoados.First().ItemDoacao.Descricao;
                            l.Quantidade = d.ItemDoacaoDoados.First().ItemDoacao.Quantidade;
                            l.TipoItem = d.ItemDoacaoDoados.First().ItemDoacao.TipoItem;

                            if (l.TipoItem == TipoItemEnum.PRODUTO)
                            {
                                l.Validade = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Produtos.First().Validade;
                                l.TipoProduto = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Produtos.First().TipoProduto;

                                if (l.TipoProduto == TipoProdutoEnum.CESTA_BASICA)
                                {
                                    l.roupa = false;
                                    l.dinheiro = false;
                                    l.mobilia = false;
                                    l.eletrodomestico = false;
                                    l.quantidade = true;
                                    l.produto = false;
                                    l.descricao = false;
                                }
                                else
                                {
                                    l.roupa = false;
                                    l.dinheiro = false;
                                    l.mobilia = false;
                                    l.eletrodomestico = false;
                                    l.quantidade = true;
                                    l.produto = true;
                                    l.descricao = true;
                                }
                            }
                            else if (l.TipoItem == TipoItemEnum.ROUPA)
                            {
                                if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().FaixaEtaria == FaixaEtariaEnum.ADULTO)
                                    l.FaixaEtaria = "Adulto";
                                else if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().FaixaEtaria == FaixaEtariaEnum.INFANTIL)
                                    l.FaixaEtaria = "Infantil";

                                l.Tamanho = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Tamanho;

                                if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Genero == GeneroEnum.MASCULINO)
                                    l.Genero = "Masculino";
                                else if (d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Genero == GeneroEnum.FEMININO)
                                    l.Genero = "Feminino";

                                l.CondicaoR = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Roupas.First().Condicao;

                                l.roupa = true;
                                l.dinheiro = false;
                                l.mobilia = false;
                                l.eletrodomestico = false;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }
                            else if (l.TipoItem == TipoItemEnum.MOBILIA)
                            {
                                l.CondicaoM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Condicao;
                                l.MedidaM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Medida;
                                l.roupa = false;
                                l.dinheiro = false;
                                l.mobilia = true;
                                l.eletrodomestico = false;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }
                            else if (l.TipoItem == TipoItemEnum.ELETRODOMESTICO)
                            {
                                l.CondicaoE = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Eletrodomesticos.First().Condicao;
                                l.MedidaM = d.ItemDoacaoDoados.First().ItemDoacao
                                    .Mobilias.First().Medida;

                                l.roupa = false;
                                l.dinheiro = false;
                                l.mobilia = false;
                                l.eletrodomestico = true;
                                l.quantidade = true;
                                l.produto = false;
                                l.descricao = true;
                            }

                        }
                        else
                        {
                            l.ItemDoacaoNome = "Doação monetária";

                            l.roupa = false;
                            l.dinheiro = true;
                            l.mobilia = false;
                            l.eletrodomestico = false;
                            l.quantidade = false;
                            l.produto = false;
                            l.descricao = false;
                        }

                        ListaDoacaos.Add(l);
                    }

                    ListaDoacaos = ListaDoacaos.OrderByDescending(x => x.DataDoacao).ToObservableCollection();
                    OnPropertyChanged(nameof(ListaDoacaos));
                }
                
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task AlterarFoto()
        {
            try
            {
                Application.Current.MainPage = new AlterarFotoPerfil();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async void CarregarUsuario()
        {
            try
            {
                int usuarioId = Preferences.Get("UsuarioId", 0);
                Usuario u = await uService.GetUsuarioAsync(usuarioId);
                Pessoa p = await pService.GetPessoaPorUsuarioAsync();
                
                EnderecoService es = new(_token);
                //Endereco e = await es.GetEnderecoAsync(p)

                Foto = u.Foto;
                Username = p.Username;
                if (u.Email == "eliane.rosa@etec.sp.gov.br")
                {
                    DescricaoMarion = "Professora querida e carismática.";
                    RuaEtec = "Rua Alcântara, 113";
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        #endregion
    }
}
