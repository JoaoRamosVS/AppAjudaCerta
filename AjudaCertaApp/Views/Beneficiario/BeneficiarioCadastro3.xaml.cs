using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class BeneficiarioCadastro3 : ContentPage
{
    UsuarioViewModel usuarioViewModel;
    Usuario usuarioAcadastrar;
    Pessoa pessoaAcadastrar;
    Endereco enderecoAcadastrar;
    public BeneficiarioCadastro3(Pessoa p, Usuario u, Endereco e)
	{
		InitializeComponent();

        pessoaAcadastrar = p;
        usuarioAcadastrar = u;
        enderecoAcadastrar = e;
        usuarioViewModel = new UsuarioViewModel(p, u, e);
        BindingContext = usuarioViewModel;
    }
}