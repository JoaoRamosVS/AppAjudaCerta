using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views;

public partial class BeneficiarioCadastro2 : ContentPage
{
    UsuarioViewModel usuarioViewModel;
    Pessoa pessoaAcadastrar;
    Usuario usuarioAcadastrar;
    public BeneficiarioCadastro2(Pessoa p, Usuario u)
	{
		InitializeComponent();
        pessoaAcadastrar = p;
        usuarioAcadastrar = u;
        usuarioViewModel = new UsuarioViewModel(p, u);
        BindingContext = usuarioViewModel;
    }
}