using AjudaCertaApp.Models;
using AjudaCertaApp.Models.Enuns;
using AjudaCertaApp.Services.Doacoes;
using AjudaCertaApp.Services.Enderecos;
using AjudaCertaApp.Services.Pessoas;
using AjudaCertaApp.Views.Doador;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AjudaCertaApp.ViewModels.Doacao
{
    public class DoacaoViewModel : BaseViewModel
    {
        private DoacaoService dService;
        private ItemDoacaoDoado idd;
        public ICommand DirecionarAgendamentoCommand { get; set; }
        public ICommand DoarItensCommand { get; set; }
        public ICommand VoltarInicioCommand { get; set; }

        public DoacaoViewModel() 
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            dService = new DoacaoService(token);
            _ = ObterConteudoDoacao();
            _ = ObterTipoProduto();
            _ = ObterGenero();
            _ = ObterFaixaEtaria();
            InicializarCommands();
        }

        public DoacaoViewModel(ItemDoacaoDoado aDoar)
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            dService = new DoacaoService(token);
            idd = aDoar;
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            DirecionarAgendamentoCommand = new Command(async () => await DirecionarParaAgendamento());
            DoarItensCommand = new Command(async () => await DoarItens());
            VoltarInicioCommand = new Command(async () => await VoltarInicio());
        }

        #region AtributosPropriedades

        #region Pickers
        private ObservableCollection<ConteudoDoacao> listaConteudoDoacao;
        public ObservableCollection<ConteudoDoacao> ListaConteudoDoacao { 
            get { return listaConteudoDoacao; }
            set 
            {
                if(value != null)
                {
                    listaConteudoDoacao = value;
                    OnPropertyChanged();
                }
            }
        }

        private ConteudoDoacao conteudoDoacaoSelecionado;
        public ConteudoDoacao ConteudoDoacaoSelecionado
        {
            get { return conteudoDoacaoSelecionado; }
            set 
            {
                if(value != null)
                {
                    conteudoDoacaoSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TipoProduto> listaTipoProduto;
        public ObservableCollection<TipoProduto> ListaTipoProduto
        {
            get { return listaTipoProduto; }
            set
            {
                if (value != null)
                {
                    listaTipoProduto = value;
                    OnPropertyChanged();
                }
            }
        }

        private TipoProduto tipoProdutoSelecionado;
        public TipoProduto TipoProdutoSelecionado
        {
            get { return tipoProdutoSelecionado; }
            set
            {
                if(value != null)
                {
                    tipoProdutoSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Genero> listaGenero;
        public ObservableCollection<Genero> ListaGenero
        {
            get { return listaGenero; }
            set
            {
                if (value != null)
                {
                    listaGenero = value;
                    OnPropertyChanged();
                }
            }
        }

        private Genero listaGeneroSelecionado;
        public Genero ListaGeneroSelecionado
        {
            get { return listaGeneroSelecionado; }
            set
            {
                if (value != null)
                {
                    listaGeneroSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<FaixaEtaria> listaFxEtaria;
        public ObservableCollection<FaixaEtaria> ListaFxEtaria
        {
            get { return listaFxEtaria; }
            set
            {
                if (value != null)
                {
                    listaFxEtaria = value;
                    OnPropertyChanged();
                }
            }
        }

        private FaixaEtaria fxEtariaSelecionado;
        public FaixaEtaria FxEtariaSelecionado
        {
            get { return fxEtariaSelecionado; }
            set
            {
                if (value != null)
                {
                    fxEtariaSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        private string nome;
        public string Nome 
        { 
            get { return nome; }
            set 
            {
                nome = value; OnPropertyChanged();
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value; OnPropertyChanged();
            }
        }

        private DateTime dataValidade;
        public DateTime DataValidade
        {
            get { return dataValidade; }
            set
            {
                dataValidade = value; OnPropertyChanged();
            }
        }

        private string medida;
        public string Medida
        {
            get { return medida; }
            set
            {
                medida = value; OnPropertyChanged();
            }
        }

        private string condicao;
        public string Condicao
        {
            get { return condicao; }
            set
            {
                condicao = value; OnPropertyChanged();
            }
        }

        private string tamanho;
        public string Tamanho
        {
            get { return tamanho; }
            set
            {
                tamanho = value; OnPropertyChanged();
            }
        }

        private double valor;
        public double Valor
        {
            get { return valor; }
            set
            {
                valor = value; OnPropertyChanged();
            }
        }

        private string quantidade;
        public string Quantidade
        {
            get { return quantidade; }
            set
            {
                quantidade = value; OnPropertyChanged();
            }
        }

        private DateTime dataAgenda;
        public DateTime DataAgenda 
        {
            get { return dataAgenda; }
            set
            {
                dataAgenda = value; OnPropertyChanged();
            }
        }
        #endregion

        #region Métodos

        #region Pickers
        public async Task ObterConteudoDoacao()
        {
            try
            {
                ListaConteudoDoacao = new ObservableCollection<ConteudoDoacao>();
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 0, Descricao = "Dinheiro" });
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 1, Descricao = "Produto" });
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 2, Descricao = "Mobilia" });
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 3, Descricao = "Roupa" });
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 4, Descricao = "Eletrodomestico" });
                ListaConteudoDoacao.Add(new ConteudoDoacao() { Id = 5, Descricao = "Cesta Básica" });

                OnPropertyChanged(nameof(ListaConteudoDoacao));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ObterTipoProduto()
        {
            ListaTipoProduto = new ObservableCollection<TipoProduto>();
            ListaTipoProduto.Add(new TipoProduto() { Id = 1, Descricao = "Alimento" });
            ListaTipoProduto.Add(new TipoProduto() { Id = 2, Descricao = "Limpeza" });
            ListaTipoProduto.Add(new TipoProduto() { Id = 3, Descricao = "Higiene" });
            OnPropertyChanged(nameof(ListaTipoProduto));
        }
        public async Task ObterGenero()
        {
            try
            {
                ListaGenero = new ObservableCollection<Genero>();
                ListaGenero.Add(new Genero() { Id = 2, Descricao = "Masculino" });
                ListaGenero.Add(new Genero() { Id = 1, Descricao = "Feminino" });
                OnPropertyChanged(nameof(ListaGenero));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task ObterFaixaEtaria()
        {
            try
            {
                ListaFxEtaria = new ObservableCollection<FaixaEtaria>();
                ListaFxEtaria.Add(new FaixaEtaria() { Id = 1, Descricao = "Adulto" });
                ListaFxEtaria.Add(new FaixaEtaria() { Id = 2, Descricao = "Infantil" });
                OnPropertyChanged(nameof(ListaFxEtaria));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion

        public async Task DirecionarParaAgendamento()
        {
            try
            {
                ItemDoacaoDoado novoIdd = new ItemDoacaoDoado();

                string token = Preferences.Get("UsuarioToken", string.Empty);
                PessoaService ps = new PessoaService(token);
                EnderecoService es = new EnderecoService(token);

                novoIdd.Doacao = new();
                novoIdd.Doacao.Pessoa = new Pessoa();
                novoIdd.Doacao.Pessoa = await ps.GetPessoaPorUsuarioAsync();
                novoIdd.Doacao.PessoaId = novoIdd.Doacao.Pessoa.Id;
                novoIdd.Doacao.Data = DateTime.Now;
                novoIdd.Doacao.StatusDoacao = StatusDoacaoEnum.PENDENTE;

                novoIdd.Doacao.Agenda = new();
                novoIdd.Doacao.Agenda.Status = StatusDoacaoEnum.PENDENTE;
                novoIdd.Doacao.Agenda.PessoaId = 1;
                novoIdd.Doacao.Agenda.EnderecoId = 1;

                novoIdd.ItemDoacao = new();
                novoIdd.ItemDoacao.Nome = Nome;
                novoIdd.ItemDoacao.Descricao = Descricao;
                novoIdd.ItemDoacao.Quantidade = 1;
                if (ConteudoDoacaoSelecionado.Id == 1)
                {
                    novoIdd.ItemDoacao.TipoItem = (TipoItemEnum)ConteudoDoacaoSelecionado.Id;
                    novoIdd.ItemDoacao.Produtos = new();
                    novoIdd.ItemDoacao.Produtos.Add(new Produto() { Validade = Convert.ToDateTime(DataValidade), StatusItem = Models.Enuns.StatusItemEnum.DISPONIVEL, TipoProduto = (TipoProdutoEnum)TipoProdutoSelecionado.Id });
                }
                else if (ConteudoDoacaoSelecionado.Id == 2)
                {
                    novoIdd.ItemDoacao.TipoItem = (TipoItemEnum)ConteudoDoacaoSelecionado.Id;
                    novoIdd.ItemDoacao.Mobilias = new();
                    novoIdd.ItemDoacao.Mobilias.Add(new Mobilia() { Medida = Medida, Condicao = Condicao, StatusItem = StatusItemEnum.DISPONIVEL });
                }
                else if (ConteudoDoacaoSelecionado.Id == 3)
                {
                    novoIdd.ItemDoacao.TipoItem = (TipoItemEnum)ConteudoDoacaoSelecionado.Id;
                    novoIdd.ItemDoacao.Roupas = new();
                    novoIdd.ItemDoacao.Roupas.Add(new Roupa() { Condicao = Condicao, StatusItem = StatusItemEnum.DISPONIVEL, Tamanho = Tamanho, Genero = (GeneroEnum)listaGeneroSelecionado.Id, FaixaEtaria = (FaixaEtariaEnum)FxEtariaSelecionado.Id  });
                }
                else if (ConteudoDoacaoSelecionado.Id == 4)
                {
                    novoIdd.ItemDoacao.TipoItem = (TipoItemEnum)ConteudoDoacaoSelecionado.Id;
                    novoIdd.ItemDoacao.Eletrodomesticos = new();
                    novoIdd.ItemDoacao.Eletrodomesticos.Add(new Eletrodomestico() { Condicao = Condicao, Medida = Medida, StatusItem = StatusItemEnum.INDISPONIVEL });
                }
                else if(ConteudoDoacaoSelecionado.Id == 5)
                {
                    novoIdd.ItemDoacao.Nome = "Cesta Básica";
                    novoIdd.ItemDoacao.Descricao = string.Empty;
                    novoIdd.ItemDoacao.TipoItem = TipoItemEnum.PRODUTO;
                    novoIdd.ItemDoacao.Quantidade = Convert.ToInt32(Quantidade);
                    novoIdd.ItemDoacao.Produtos = new();
                    novoIdd.ItemDoacao.Produtos.Add(new Produto() { Validade = DateTime.MaxValue, StatusItem = Models.Enuns.StatusItemEnum.DISPONIVEL, TipoProduto = TipoProdutoEnum.CESTA_BASICA });
                }

                await Application.Current.MainPage
                    .Navigation.PushAsync(new AgendarDoacao(novoIdd));
                    
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DoarItens() 
        {
            try
            {
                if(DataAgenda < DateTime.Now)
                    await Application.Current.MainPage
                    .DisplayAlert("Erro", "Não é possível agendar doações em datas inválidas.", "Ok");

                ItemDoacaoDoado aDoar = idd;
                aDoar.Doacao.Agenda.Data = DataAgenda;

                int idDoacao = await dService.PostDoacaoItens(aDoar);
                if(idDoacao != 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new AgendamentoConcluido());
                }
                else
                    await Application.Current.MainPage
                    .DisplayAlert("Erro", "Tente novamente mais tarde.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task VoltarInicio()
        {
            try
            {
                PessoaService p = new(Preferences.Get("UsuarioToken", string.Empty));
                Pessoa pessoaLogada = await p.GetPessoaPorUsuarioAsync();

                if(pessoaLogada != null) 
                {
                    Application.Current.MainPage = new AppShell(pessoaLogada);
                    
                }
                else
                    await Application.Current.MainPage
                    .DisplayAlert("Erro", "Tente novamente mais tarde.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
