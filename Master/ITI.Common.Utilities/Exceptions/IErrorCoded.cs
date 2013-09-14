using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Common.Utilities.Exceptions
{
    /// <summary>
    /// Interface that can be implemented by exceptions etc that are error coded.
    /// </summary>
    /// <remarks>
    /// <p>
    /// The error code is a <see cref="System.String"/>, rather than a number, so it can
    /// be given user-readable values, such as "object.failureDescription".
    /// </p>
    /// </remarks>
    /// <author>Ahmed Al Amir</author>    
    public interface IErrorCoded
    {
        /// <summary>
        /// Return the error code associated with this failure.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The GUI can render this anyway it pleases, allowing for I18n etc.
        /// </p>
        /// </remarks>
        /// <returns>
        /// The <see cref="System.String"/> error code associated with this failure,
        /// or the empty string instance if not error-coded.
        /// </returns>
        string ErrorCode
        {
            get;
        }
    }
}
