using AjudaCertaApp.ViewModels.Posts;
using AjudaCertaApp.ViewModels.Usuarios;

namespace AjudaCertaApp.Views.Ong;

public partial class PostView : ContentPage
{
	PostViewModel viewModel;
	public PostView()
	{
		InitializeComponent();

		viewModel = new PostViewModel();
		BindingContext = viewModel;
	}
}