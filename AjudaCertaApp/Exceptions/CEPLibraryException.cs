using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.Exceptions
{
    public class CEPLibraryException : Exception
    {
        public CEPLibraryException()
        {
        }

        public CEPLibraryException(string message)
            : base(message)
        {
        }

        public CEPLibraryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
