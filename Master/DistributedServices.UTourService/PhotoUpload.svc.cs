using System;
using System.ServiceModel;
using DistributedServices.UTourService.InstanceProvider;
using ITI.Common.HotSpotsInfo;
using Application.Contracts;
using DistributedServices.Contracts;
using ITI.Common.HotSpotsInfo.CommonCotracts;

namespace DistributedServices.UTourService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PhotoUpload" in code, svc and config file together.
     [UnityInstanceProviderServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PhotoUpload : IPhotoUpload
    {
        private IUploadPhotosManagementService UploadPhotosService;


        public PhotoUpload(IUploadPhotosManagementService UploadPhotosService)
        {
            if (UploadPhotosService == (IUploadPhotosManagementService)null)
                throw new ArgumentNullException("UploadPhotosManagementService");

            this.UploadPhotosService = UploadPhotosService;
        }



        [OperationBehavior]
        public OperationResult UploadPhoto(string userName, string hotSpotID, byte[] ImageBytes)
        {
            string errorMessage;
            bool isValid = UploadPhotosService.SavePhoto(userName, hotSpotID, ImageBytes, out errorMessage);
            return new OperationResult() {ErrorMessage = errorMessage, OperationStatus = isValid};
        }
    }
}
