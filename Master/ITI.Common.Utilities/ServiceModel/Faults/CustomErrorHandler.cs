using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.Utilities.ServiceModel.Faults.Interfaces;

namespace ITI.Common.Utilities.ServiceModel.Faults
{
    /// <summary>
    /// Customized class which handles error in a specific mechanism
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public class CustomErrorHandler : ICustomErrorHandler
    {
        #region ICustomErrorHandler Members
        /// <summary>
        /// Handle and Log occured exception
        /// </summary>
        /// <param name="error">Exception to handle</param>
        public void HandleError(Exception error)
        {
            ErrorHandlerHelper.LogError(error);      
        }

        /// <summary>
        /// Handle and Log occured exception
        /// </summary>
        /// <param name="error">Exception to handle</param>
        /// <param name="version"></param>
        /// <param name="fault"></param>
        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            ErrorHandlerHelper.PromoteException(error,version,ref fault);      
        }

        #endregion
    }
}
