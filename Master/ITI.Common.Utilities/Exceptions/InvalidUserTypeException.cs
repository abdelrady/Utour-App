using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.Utilities.Exceptions
{

    /// <summary>
    /// 
    /// </summary>
    public class InvalidUserTypeException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidUserTypeException(string message)
            : base(message)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InvalidUserTypeException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
