using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionControl
{
    [Serializable]
    public class TypeNotRegisteredException : Exception
    {
        /// <summary>
        /// The exception that is thrown when there is an attempt to resolve an unregistered type
        /// </summary>
        public TypeNotRegisteredException(string message) : base(message) { }
    }
}
