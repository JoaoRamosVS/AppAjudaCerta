using AjudaCertaApp.ViewModels.Perfil;

namespace AjudaCertaApp.Views.Doador;

public partial class PerfilDoador : ContentPage
{
	ListagemDoacoesViewModel viewModel;
	public PerfilDoador()
	{
		InitializeComponent();
		viewModel = new ListagemDoacoesViewModel();
		BindingContext = viewModel;
	}
}