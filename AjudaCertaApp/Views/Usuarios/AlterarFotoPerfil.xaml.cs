using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views.Usuarios;

public partial class AlterarFotoPerfil : ContentPage
{
	ImagemUsuarioViewModel viewModel;
	public AlterarFotoPerfil()
	{
		InitializeComponent();
		viewModel = new();
		BindingContext = viewModel;
	}
}