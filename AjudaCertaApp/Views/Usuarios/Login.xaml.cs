using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class Login : ContentPage
{
	UsuarioViewModel usuarioViewModel;
	public Login()
	{
		InitializeComponent();
		usuarioViewModel = new UsuarioViewModel();
		BindingContext = usuarioViewModel;
	}
}