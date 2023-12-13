using AjudaCertaApp.Exceptions;
using AjudaCertaApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Services.Pessoas
{
    public class Search
    {
        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="zipCode">Zip code value</param>
        /// <param name="type">The type to search address. Use ViaCEPTypes object to help. Possible values include: 'json', 'xml', 'piped' and 'querty'</param>
        /// <returns>String with result in type selected</returns>
        /// 
        public static string ByZipCode(string zipCode, string type)
        {
            try
            {
                var result = ViaCEPServices.GetAddressByCEP(zipCode, type);

                return result;
            }
            catch (CEPLibraryException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CEPLibraryException(ex.Message);
            }
        }

        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="zipCode">Zip code value</param>
        /// <returns>Object with address result</returns>
        public static ViaCEPModel ByZipCode(string zipCode)
        {
            try
            {
                var jsonResult = ViaCEPServices.GetAddressByCEP(zipCode, "json");

                var objectResult = JsonConvert.DeserializeObject<ViaCEPModel>(jsonResult);

                return objectResult;
            }
            catch (CEPLibraryException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CEPLibraryException(ex.Message);
            }
        }
    }
}
