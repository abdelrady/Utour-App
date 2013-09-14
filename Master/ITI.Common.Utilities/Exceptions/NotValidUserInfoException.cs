using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.Utilities.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class NotValidUserInfoException : Exception
    {
        /// <summary>
        /// Not Valid User Info Exception , that is thrown when the user input not valid data
        /// like spaces , invalid characters , or empty values
        /// </summary>
        /// <param name="message">the message to pass to the upper layer</param>
        public NotValidUserInfoException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Not Valid User Info Exception , that is thrown when the user input not valid data
        /// like spaces , invalid characters , or empty values
        /// </summary>
        /// <param name="message">the message to pass to the upper layer</param>
        /// <param name="inner">the inner exception that cause this exception to occur</param>
        public NotValidUserInfoException(string message , Exception inner) : base(message , inner)
        {
        }
    }
}
