using AjudaCertaApp.Models;
using AjudaCertaApp.Models.Enuns;
using AjudaCertaApp.Services;
using AjudaCertaApp.Services.Pessoas;
using AjudaCertaApp.Services.Usuarios;
using AjudaCertaApp.Utils;
using AjudaCertaApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AjudaCertaApp.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;
        private PessoaService pService;
        private ViaCEPServices vcService;
        public ICommand DirecionarCadastroCommand { get; set; }
        public ICommand DirecionarLoginCommand { get; set; }
        public ICommand DirecionarCadastroDoador1Command { get; set; }
        public ICommand DirecionarCadastroDoador2Command { get; set; }
        public ICommand DirecionarCadastroDoador3Command { get; set; }
        public ICommand PreencherEnderecoCommand { get; set; }
        public ICommand DirecionarCadastroBeneficiario1Command { get; set; }
        public ICommand DirecionarCadastroBeneficiario2Command { get; set; }
        public ICommand DirecionarCadastroBeneficiario3Command { get; set; }
        public ICommand VoltarCommand { get; set; }
        public ICommand RegistrarPessoaCommand { get; set; }
        public ICommand AutenticarPessoaCommand { get; set; }
        public Pessoa pessoaCadastro { get; set; }
        public Usuario usuarioCadastro { get; set;}
        public Endereco enderecoCadastro { get; set; }
        public UsuarioViewModel()
        {
            pService = new PessoaService();
            uService = new UsuarioService();
            vcService = new ViaCEPServices();
            InicializarCommands();
            _ = ObterFisicaJuridica();
            _ = ObterGenero();
        }
        public UsuarioViewModel(Pessoa p, Usuario u)
        {
            pService = new PessoaService();
            uService = new UsuarioService();
            vcService = new ViaCEPServices();
            InicializarCommands();
            _ = ObterFisicaJuridica();
            _ = ObterGenero();
            pessoaCadastro = p;
            usuarioCadastro = u;
        }
        public UsuarioViewModel(Pessoa p, Usuario u, Endereco e)
        {
            pService = new PessoaService();
            vcService = new ViaCEPServices();
            uService = new UsuarioService();
            InicializarCommands();
            _ = ObterFisicaJuridica();
            _ = ObterGenero();
            pessoaCadastro = p;
            usuarioCadastro = u;
            enderecoCadastro = e;
        }

        public void InicializarCommands()
        {
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
            DirecionarLoginCommand = new Command(async () => await DirecionarParaLogin());
            DirecionarCadastroDoador1Command = new Command(async () => await DirecionarParaCadastroDoador1());
            DirecionarCadastroDoador2Command = new Command(async () => await DirecionarParaCadastroDoador2());
            DirecionarCadastroDoador3Command = new Command(async () => await DirecionarParaCadastroDoador3());
            PreencherEnderecoCommand = new Command(async () => await PreencherEndereço());
            DirecionarCadastroBeneficiario1Command = new Command(async () => await DirecionarParaCadastroBeneficiario1());
            DirecionarCadastroBeneficiario2Command = new Command(async () => await DirecionarParaCadastroBeneficiario2());
            DirecionarCadastroBeneficiario3Command = new Command(async () => await DirecionarParaCadastroBeneficiario3());
            VoltarCommand = new Command(async () => await Voltar());
            RegistrarPessoaCommand = new Command(async () => await CadastrarPessoa());
            AutenticarPessoaCommand = new Command(async () => await  AutenticarPessoa());
        }

        #region AtributosPropriedades


        private bool btnEntrar = true;
        public bool BtnEntrar
        {
            get { return btnEntrar; }
            set 
            {
                btnEntrar = value;
                OnPropertyChanged();
            }
        }
        private string nome = string.Empty; // ctrl + r, e

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }

        private DateTime datanasc = DateTime.Now;
        public DateTime Datanasc
        {
            get { return datanasc; }
            set
            {
                datanasc = value;
                OnPropertyChanged();
            }
        }

        private string genero = string.Empty;
        public string Genero
        {
            get { return genero; }
            set
            {
                genero = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FisicaJuridica> listaFisicaJuridica;
        public ObservableCollection<FisicaJuridica> ListaFisicaJuridica
        {
            get { return listaFisicaJuridica; }
            set
            {
                if (value != null)
                {
                    listaFisicaJuridica = value;
                    OnPropertyChanged();
                }
            }
        }

        private FisicaJuridica fisicaJuridicaSelecionado;
        public FisicaJuridica FisicaJuridicaSelecionado
        {
            get { return fisicaJuridicaSelecionado; }
            set
            {
                if (value != null)
                {
                    fisicaJuridicaSelecionado = value;
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


        private string username = string.Empty;

        public string Username { get { return username;  }
            set 
            {
                username = value;
                OnPropertyChanged();
            }
                }

        private string email = string.Empty;

        public string Email { get { return email; }
            set 
            {
                email = value; 
                OnPropertyChanged(); 
            }
                }
        
        private string cpf = string.Empty;
        public string Cpf
        {
            get { return cpf; }
            set 
            { 
                cpf = value; 
                OnPropertyChanged(); 
            }
        }

        private string cnpj = string.Empty;
        public string Cnpj
        {
            get { return cnpj; }
            set
            {
                cnpj = value;
                OnPropertyChanged();
            }
        }
        
        private string telefone = string.Empty;
        public string Telefone
        {
            get { return telefone; }
            set
            {
                telefone = value;
                OnPropertyChanged();
            }
        }

        private string ceep = string.Empty;

        public string Ceep { get { return ceep; }
            set 
            {
                ceep = value;
                OnPropertyChanged();
            }
        }

        private string rua = string.Empty;
        public string Rua { get { return rua; }
        set 
            {
                rua = value;
                OnPropertyChanged();
            }
        }

        private string bairro = string.Empty;
        public string Bairro { get { return bairro; }
            set 
            {
                bairro = value;
                OnPropertyChanged();
            }
        }

        private string cidade = string.Empty;
        public string Cidade { get { return cidade; }
            set 
            {
                cidade = value;
                OnPropertyChanged();
            }
        }
        
        private string estado = string.Empty;
        public string Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                OnPropertyChanged();
            }
        }

        private string numero = string.Empty;
        public string Numero { get { return numero; }
            set 
            {
                numero = value;
                OnPropertyChanged();
            }
        }

        private string complemento = string.Empty;
        public string Complemento
        {
            get { return complemento; }
            set
            {
                complemento = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha { get { return senha; }
            set 
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        private string confirmaSenha = string.Empty;
        public string ConfirmaSenha
        {
            get { return confirmaSenha; }
            set
            {
                confirmaSenha = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Métodos
        public async Task ObterFisicaJuridica()
        {
            try
            {
                ListaFisicaJuridica = new ObservableCollection<FisicaJuridica>();
                ListaFisicaJuridica.Add(new FisicaJuridica() { Id = 1, Descricao = "Pessoa Física" });
                ListaFisicaJuridica.Add(new FisicaJuridica() { Id = 2, Descricao = "Pessoa Jurídica" });
                OnPropertyChanged(nameof(ListaFisicaJuridica));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ObterGenero()
        {
            try
            {
                ListaGenero = new ObservableCollection<Genero>();
                ListaGenero.Add(new Genero() { Id = 1, Descricao = "Masculino" });
                ListaGenero.Add(new Genero() { Id = 2, Descricao = "Feminino" });
                ListaGenero.Add(new Genero() { Id = 3, Descricao = "Outros" });
                ListaGenero.Add(new Genero() { Id = 4, Descricao = "Prefiro não informar" });
                OnPropertyChanged(nameof(ListaGenero));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.CadastroEscolha());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaLogin()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task Voltar()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastroDoador1()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.DoadorCadastro1());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastroDoador2()
        {
            try
            {
                Pessoa p = new Pessoa();
                p.Nome = Nome;
                p.DataNasc = Convert.ToDateTime(Datanasc);
                p.fisicaJuridica = (FisicaJuridicaEnum)fisicaJuridicaSelecionado.Id;
                p.Tipo = TipoPessoaEnum.DOADOR;
                p.Username = Username;
                if(fisicaJuridicaSelecionado.Id == 1)
                {
                    p.Documento = Cpf;
                    if(!Validacao.ValidaCPF(Cpf))
                        await Application.Current.MainPage
                    .DisplayAlert("Atenção", "O CPF informado não é válido.", "Ok");
                }
                else if(fisicaJuridicaSelecionado.Id == 2)
                {
                    p.Documento = Cnpj;
                    if(!Validacao.ValidaCNPJ(Cnpj))
                        await Application.Current.MainPage
                    .DisplayAlert("Atenção", "O CNPJ informado não é válido.", "Ok");
                }


                Usuario u = new Usuario();
                u.Email = Email;
                if(!Validacao.VerificaEmail(Email))
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "O email informado não é válido.", "Ok");
                else if(!Validacao.VerificaMaioridade(Datanasc))
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "O usuário precisa ser maior de idade", "Ok");

                #region Validações
                if (Nome == string.Empty)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha o campo nome.", "Ok");
                }
                else if (Username == string.Empty)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha o campo usuário.", "Ok");
                }
                else if (Email == string.Empty)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha o campo email.", "Ok");
                }
                else if (Telefone == string.Empty)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "Preencha o campo telefone.", "Ok");
                }
                else if (Datanasc == DateTime.Now)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "Escolha sua data de nascimento.", "Ok");
                }
                else if (Cpf == string.Empty && Cnpj == string.Empty)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Atenção", "É necessário informar seu CPF/CNPJ.", "Ok");
                }
                else
                    #endregion

                    await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.DoadorCadastro2(p, u));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastroDoador3()
        {
            try
            {
                Pessoa p = pessoaCadastro;
                Usuario u = usuarioCadastro;

                Endereco e = new Endereco();
                e.Cep = Ceep;
                e.Rua = Rua;
                e.Estado = Estado;
                e.Cidade = Cidade;
                e.Bairro = Bairro;
                e.Complemento = Complemento;
                e.Numero = Numero;
                

                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.DoadorCadastro3(p, u, e));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }


        public async Task DirecionarParaCadastroBeneficiario1()
        {
            try
            {
                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.BeneficiarioCadastro1());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastroBeneficiario2()
        {
            try
            {
                Pessoa p = new Pessoa();
                p.Nome = Nome;
                p.Documento = Cpf;
                p.Username = Username;
                p.Telefone = Telefone;
                p.fisicaJuridica = FisicaJuridicaEnum.PESSOA_FISICA;
                p.Genero = ListaGeneroSelecionado.Descricao;
                p.Tipo = TipoPessoaEnum.BENEFICIARIO;

                Usuario u = new Usuario();
                u.Email = Email;

                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.BeneficiarioCadastro2(p, u));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastroBeneficiario3()
        {
            try
            {
                Pessoa p = pessoaCadastro;
                Usuario u = usuarioCadastro;

                Endereco e = new Endereco();
                e.Rua = Rua;
                e.Cidade = Cidade;
                e.Cep = Ceep;
                e.Complemento = Complemento;
                e.Estado = Estado;
                e.Numero = Numero;
                e.Bairro = Bairro;

                await Application.Current.MainPage
                    .Navigation.PushAsync(new Views.BeneficiarioCadastro3(p, u, e));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }


        public async Task PreencherEndereço()
        {
            try
            {
                string cepDigitado = Ceep;
                if(cepDigitado.Length == 8) 
                {
                    ViaCEPModel objectResult = Search.ByZipCode(cepDigitado);
                    if (objectResult != null)
                    {
                        Rua = objectResult.Address1;
                        Bairro = objectResult.Neighborhood;
                        Cidade = objectResult.City;
                        Estado = objectResult.State;
                    }
                }
                else
                    await Application.Current.MainPage
                        .DisplayAlert("Atenção", "O CEP digitado não é válido.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task CadastrarPessoa()
        {
            try
            {
                Pessoa p = pessoaCadastro;
                Usuario u = usuarioCadastro;
                Endereco e = enderecoCadastro;
               // e.Cep = "02202-000"; remover validação de cep da api

                if(Senha != null && ConfirmaSenha != null)
                {
                    if(Senha == ConfirmaSenha)
                    {
                        u.Senha = Senha;

                        p.Usuario = u;
                        p.Endereco = e;

                        Pessoa pRegistrado = await pService.PostRegistrarPessoaAsync(p);
                        if(pRegistrado.Id != 0)
                        {
                            string mensagem = "Cadastro realizado com sucesso! Agora, realize a autenticação.";
                            await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");
                            await Application.Current.MainPage.Navigation.PopToRootAsync();
                        }
                    }
                    else
                        await Application.Current.MainPage
                            .DisplayAlert("Atenção", "As senhas não conferem.", "Ok");
                }
                else
                    await Application.Current.MainPage
                        .DisplayAlert("Atenção","Preencha os campos corretamente.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task AutenticarPessoa()
        {
            try
            {
                BtnEntrar = false; 
                Usuario credenciais = new Usuario();
                credenciais.Senha = Senha;
                credenciais.Email = Email;

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(credenciais);
                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioEmail", uAutenticado.Email);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    PessoaService ps = new PessoaService(uAutenticado.Token);
                    Pessoa p = await ps.GetPessoaPorUsuarioAsync();
                    if (p == null) 
                    {
                        await Application.Current.MainPage
                            .DisplayAlert("Erro", "Falha", "Ok");
                        BtnEntrar = true;
                    }
                    else
                    { 
                        Application.Current.MainPage = new AppShell(p);
                    }
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Erro", "Dados incorretos. Verifique o email e a senha.", "Ok");
                    BtnEntrar = true;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
                BtnEntrar = true;
            }
        }
        #endregion
    }
}
