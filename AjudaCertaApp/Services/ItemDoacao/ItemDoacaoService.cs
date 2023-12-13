using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.ItemDoacao
{
    public class ItemDoacaoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/ItemDoacao";
        private string _token;

        public ItemDoacaoService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Models.ItemDoacao> GetPorId(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/{0}", itemDoacaoId);
            Models.ItemDoacao ItmD = await _request.GetAsync<Models.ItemDoacao>(apiUrlBase + urlComplementar, _token);

            return ItmD;
        }

        public async Task<Produto> GetProdutoPorId(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/GetProduto/{0}", itemDoacaoId);
            Produto p = await _request.GetAsync<Produto>(apiUrlBase + urlComplementar, _token);

            return p;
        }

        public async Task<Roupa> GetRoupaPorId(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/GetRoupa/{0}", itemDoacaoId);
            Roupa r = await _request.GetAsync<Roupa>(apiUrlBase + urlComplementar, _token);

            return r;
        }

        public async Task<Mobilia> GetMobiliaPorId(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/GetMobilia/{0}", itemDoacaoId);
            Mobilia m = await _request.GetAsync<Mobilia>(apiUrlBase + urlComplementar, _token);

            return m;
        }

        public async Task<Eletrodomestico> GetEletrodomesticoPorId(int itemDoacaoId)
        {
            string urlComplementar = string.Format("/GetEletrodomestico/{0}", itemDoacaoId);
            Eletrodomestico e = await _request.GetAsync<Eletrodomestico>(apiUrlBase + urlComplementar, _token);

            return e;
        }
    }
}
