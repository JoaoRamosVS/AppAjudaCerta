using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Posts
{
    public class PostService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/Posts";
        private string _token;

        public PostService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Post>> GetPostsAsync()
        {
            string urlComplementar = string.Format("{0}", "/ListarPosts");
            ObservableCollection<Models.Post> listaPosts = await
                _request.GetAsync<ObservableCollection<Models.Post>>(apiUrlBase + urlComplementar, _token);
            return listaPosts;
        }

        public async Task<int> PostPostAsync(Post p)
        {
            string urlComplementar = string.Format("{0}", "/AdicionarPost");
            return await _request.PostReturnIntTokenAsync(apiUrlBase + urlComplementar, p, _token);
        }
    }
}
