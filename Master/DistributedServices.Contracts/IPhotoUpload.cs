using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ITI.Common.HotSpotsInfo;
using ITI.Common.HotSpotsInfo.CommonCotracts;

namespace DistributedServices.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPhotoUpload" in both code and config file together.
    [ServiceContract]
    public interface IPhotoUpload
    {
        [OperationContract]
        OperationResult UploadPhoto(string userName, string hotSpotID, byte [] ImageBytes);
    }
}
