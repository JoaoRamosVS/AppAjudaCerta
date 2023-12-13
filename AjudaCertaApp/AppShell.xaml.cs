using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels;

namespace AjudaCertaApp
{
    public partial class AppShell : Shell
    {
        Pessoa pessoaLogada;
        AppShellViewModel viewModel;
        public AppShell(Pessoa pessoalogada)
        {
            InitializeComponent();
            viewModel = new AppShellViewModel();
            BindingContext = viewModel;
            pessoaLogada = pessoalogada;
            
            if(pessoaLogada.Tipo == Models.Enuns.TipoPessoaEnum.ONG) 
            {
                btnPostar.IsVisible = true;
                BtnDoar.IsVisible = false;

                BtnReservar.IsVisible = false;
                btnPerfilOng.IsVisible = true;
                btnPerfilDoador.IsVisible = false;
                btnPerfilBeneficiario.IsVisible = false;
            }
            else if(pessoaLogada.Tipo == Models.Enuns.TipoPessoaEnum.DOADOR)
            {
                btnPostar.IsVisible = false;
                BtnDoar.IsVisible = true;
                BtnReservar.IsVisible = false;

                btnPerfilOng.IsVisible = false;
                btnPerfilDoador.IsVisible = true;
                btnPerfilBeneficiario.IsVisible = false;
            }
            else if(pessoaLogada.Tipo == Models.Enuns.TipoPessoaEnum.BENEFICIARIO)
            {
                btnPostar.IsVisible = false;
                BtnDoar.IsVisible = false;
                BtnReservar.IsVisible = true;

                btnPerfilOng.IsVisible = false;
                btnPerfilDoador.IsVisible = false;
                btnPerfilBeneficiario.IsVisible = true;
            }
        }
    }
}
