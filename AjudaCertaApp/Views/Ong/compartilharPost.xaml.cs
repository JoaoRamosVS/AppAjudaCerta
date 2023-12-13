using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels.Doacao;
using AjudaCertaApp.ViewModels.Posts;

namespace AjudaCertaApp.Views.Ong;

public partial class compartilharPost : ContentPage
{
	PostViewModel viewModel;
	public compartilharPost(Post post, ImageSource imagem)
	{
        InitializeComponent();
        viewModel = new(post, imagem);
		BindingContext = viewModel;
	}
}