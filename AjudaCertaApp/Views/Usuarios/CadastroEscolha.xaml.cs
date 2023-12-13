using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class CadastroEscolha : ContentPage
{
	UsuarioViewModel usuarioViewModel;
	public CadastroEscolha()
	{
		InitializeComponent();
		usuarioViewModel = new UsuarioViewModel();
		BindingContext = usuarioViewModel;
	}

    private void Image_SizeChanged(object sender, EventArgs e)
    {

    }
}