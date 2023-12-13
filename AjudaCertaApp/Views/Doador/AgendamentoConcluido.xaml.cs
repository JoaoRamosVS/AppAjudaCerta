using AjudaCertaApp.ViewModels.Doacao;

namespace AjudaCertaApp.Views.Doador;

public partial class AgendamentoConcluido : ContentPage
{
	DoacaoViewModel viewModel;
	public AgendamentoConcluido()
	{
		InitializeComponent();
		viewModel = new();
		BindingContext = viewModel;
	}
}