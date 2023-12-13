using AjudaCertaApp.ViewModels.Perfil;

namespace AjudaCertaApp.Views.Ong;

public partial class PerfilOng : ContentPage
{
	ListagemDoacoesViewModel viewModel;
	public PerfilOng()
	{
		InitializeComponent();
		viewModel = new ListagemDoacoesViewModel();
		BindingContext = viewModel;
		
		
	}
}