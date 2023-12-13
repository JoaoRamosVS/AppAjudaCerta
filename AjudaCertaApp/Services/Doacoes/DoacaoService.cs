using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Doacoes
{
    public class DoacaoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/Doacao";
        private string _token;

        public DoacaoService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Doacao>> GetDoacoesAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Doacao> listaDoacoes = await
                _request.GetAsync<ObservableCollection<Doacao>>(apiUrlBase + urlComplementar, _token);
            return listaDoacoes;
        }

        public async Task<ObservableCollection<Doacao>> GetDoacoesByPessoa()
        {
            string urlComplementar = string.Format("{0}", "/GetByUser");
            ObservableCollection<Doacao> listaDoacoes = await
                _request.GetAsync<ObservableCollection<Doacao>>(apiUrlBase + urlComplementar, _token);
            return listaDoacoes;
        }

        public async Task<int> PostDoacaoItens(Models.ItemDoacaoDoado novoIDD) 
        {
            string urlComplementar = string.Format("{0}", "/DoacaoItens");
            return await _request.PostReturnIntTokenAsync(apiUrlBase + urlComplementar, novoIDD, _token);
        }
    }
}
