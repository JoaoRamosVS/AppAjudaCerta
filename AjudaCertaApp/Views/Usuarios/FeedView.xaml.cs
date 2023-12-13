using AjudaCertaApp.ViewModels.Posts;

namespace AjudaCertaApp.Views;

public partial class FeedView : ContentPage
{
	ListagemPostViewModel viewModel;
	public FeedView()
	{
		InitializeComponent();
		
		viewModel = new ListagemPostViewModel();
		BindingContext = viewModel;
	}

    private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
			viewModel.ObterPosts();
    }
}