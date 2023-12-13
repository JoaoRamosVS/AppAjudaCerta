using AjudaCertaApp.Models;
using AjudaCertaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Pessoas
{
    public class PessoaService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/Pessoas";
        private string _token;  

        public PessoaService()
        {
            _request = new Request();
        }

        public PessoaService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Pessoa> PostRegistrarPessoaAsync(Pessoa p)
        {
            string urlComplementar = "/Registrar";
            p.Id = await _request.PostReturnIntAsync(apiUrlBase + urlComplementar, p);

            return p;
        }

        public async Task<Pessoa> GetPessoaPorUsuarioAsync()
        {
            string urlComplementar = "/GetByUsuarioId";
            Pessoa p = await _request.GetAsync<Pessoa>(apiUrlBase + urlComplementar, _token);

            return p;
        }

        public async Task<Pessoa> GetPessoaPorId(int pessoaId)
        {
            string urlComplementar = string.Format("/{0}", pessoaId);
            Pessoa p = await _request.GetAsync<Pessoa>(apiUrlBase + urlComplementar, _token);

            return p;
        }

    }
}
