using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.ItemDoacaoDoado
{
    public class ItemDoacaoDoadoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/ItemDoacaoDoado";
        private string _token;

        public ItemDoacaoDoadoService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<AjudaCertaApp.Models.ItemDoacaoDoado> GetPorIdDoacao(int doacaoId)
        {
            string urlComplementar = string.Format("/IddByIdDoacao/{0}", doacaoId);
            Models.ItemDoacaoDoado itemDoacaoDoado = await _request.GetAsync<Models.ItemDoacaoDoado>(apiUrlBase + urlComplementar, _token);

            return itemDoacaoDoado;
        }

        public async Task<Models.ItemDoacaoDoado> GetPorIdItemDoacao(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/IddByIdItemDoacao/{0}", itemDoacaoId);
            Models.ItemDoacaoDoado itemDoacaoDoado = await _request.GetAsync<Models.ItemDoacaoDoado>(apiUrlBase + urlComplementar, _token);

            return itemDoacaoDoado;
        }
    }
}
