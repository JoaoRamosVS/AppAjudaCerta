using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Enderecos
{
    public class EnderecoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/Enderecos";
        private string _token;
        public EnderecoService(string token) 
        {
            _token = token;
            _request = new Request();
        }

        public async Task<Endereco> GetEnderecoAsync(int endId)
        {
            string urlComplementar = string.Format("/{0}", endId);
            Endereco e = await _request.GetAsync<Endereco>(apiUrlBase + urlComplementar, _token);

            return e;
        }
    }
}
