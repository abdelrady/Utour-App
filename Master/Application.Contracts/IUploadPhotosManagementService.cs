using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts
{
    public interface IUploadPhotosManagementService
    {
        bool SavePhoto(string UserName, string hotSpotID, byte[] imageData, out string errorMessage);
    }
}
