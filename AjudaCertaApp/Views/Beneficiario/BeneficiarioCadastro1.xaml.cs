using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class BeneficiarioCadastro1 : ContentPage
{
	UsuarioViewModel usuarioViewModel;
	public BeneficiarioCadastro1()
	{
		InitializeComponent();

		usuarioViewModel = new UsuarioViewModel();
		BindingContext = usuarioViewModel;
	}
}