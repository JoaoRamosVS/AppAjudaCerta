using AjudaCertaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Agendas
{
    public class AgendaService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://ajudacerta-001-site1.anytempurl.com/Agendas";
        private string _token;

        public AgendaService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Agenda> GetAgendaPorId(int agendaId)
        {
            string urlComplementar = string.Format("/{0}", agendaId);
            Agenda a = await _request.GetAsync<Agenda>(apiUrlBase + urlComplementar, _token);

            return a;
        }
    }
}
