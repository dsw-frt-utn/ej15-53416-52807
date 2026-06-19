using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Exceptions
{
<<<<<<< HEAD
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
=======
    internal class ValidationException : Exception
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }
>>>>>>> 6afcaf6cb79b66edd983ea1e08b52c287ffa2715
    }
}
