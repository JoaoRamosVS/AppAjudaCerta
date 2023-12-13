using AjudaCertaApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services
{
    public class ViaCEPServices
    {
        public static string GetAddressByCEP(string cep, string type)
        {
            try
            {
                string result = string.Empty;

                string viaCEPUrl = $"https://viacep.com.br/ws/{cep}/{type}/";

                WebClient client = new WebClient();
                result = client.DownloadString(viaCEPUrl);

                return result;
            }
            catch (Exception ex)
            {
                throw new CEPLibraryException(ex.Message);
            }
        }
    }
}
