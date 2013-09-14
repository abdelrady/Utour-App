using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace ITI.Common.Utilities.ServiceModel.Faults.Interfaces
{
    public interface ICustomErrorHandler
    {
        void HandleError(Exception error);
        void ProvideFault(Exception error, MessageVersion version, ref Message fault);
    }
}
