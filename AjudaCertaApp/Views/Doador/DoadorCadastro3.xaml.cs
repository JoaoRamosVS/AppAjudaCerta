using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class DoadorCadastro3 : ContentPage
{
	UsuarioViewModel usuarioViewModel;
	Endereco enderecoAcadastrar;
	Pessoa pessoaAcadastrar;
	Usuario usuarioAcadastrar;
	public DoadorCadastro3(Pessoa p, Usuario u, Endereco e)
	{
		InitializeComponent();

		pessoaAcadastrar = p;
		usuarioAcadastrar = u;
		enderecoAcadastrar = e;
		usuarioViewModel = new UsuarioViewModel(p, u, e);
		BindingContext = usuarioViewModel;
	}
}