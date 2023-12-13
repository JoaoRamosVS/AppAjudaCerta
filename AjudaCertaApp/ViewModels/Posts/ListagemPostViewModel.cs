using AjudaCertaApp.Models;
using AjudaCertaApp.Services.Posts;
using Syncfusion.Maui.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AjudaCertaApp.ViewModels.Posts
{
    public class ListagemPostViewModel : BaseViewModel
    {
        private PostService pService;
        public ObservableCollection<Post> Posts { get; set; }
        public ListagemPostViewModel() 
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PostService(token);
            Posts = new ObservableCollection<Post>();

            _ = ObterPosts();
            AtualizarCommand = new Command(async () => ObterPosts());
        }

        public ICommand AtualizarCommand { get; set; }

        public async Task ObterPosts()
        {
            try
            {
                Posts = await pService.GetPostsAsync();
                Posts = Posts.OrderByDescending(x => x.DataPostagem).ToObservableCollection();
                OnPropertyChanged(nameof(Posts));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
