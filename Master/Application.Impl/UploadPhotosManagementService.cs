using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using Application.Contracts;
using System.IO;
using Domain.Contracts;
using Domain.DataContracts;

namespace Application.Impl
{
    public class UploadPhotosManagementService : IUploadPhotosManagementService
    {
        #region private members

        private ITouristRepository _touristRepository;
        private IUploadedPhotoRepository _photoRepository;

        public UploadPhotosManagementService(ITouristRepository touristRepository, IUploadedPhotoRepository photoRepository)
        {
            if (touristRepository != null) _touristRepository = touristRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "touristRepository");
            if (photoRepository != null) _photoRepository = photoRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "photoRepository");
        }

        #endregion

        public bool SavePhoto(string UserName, string hotSpotID, byte[] imageData, out string errorMessage)
        {
            var parentDir = ConfigurationManager.AppSettings["SaveImagePath"].ToString();

            var imageDir = "\\" + "UploadedImages\\" + UserName + hotSpotID;
            var imagePath = imageDir + "\\" +
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '_').Replace(' ', '_').Replace(':', '_') + ".jpg";
            if (!Directory.Exists(/*Directory.GetCurrentDirectory() +*/ parentDir + imageDir))
            {
                Directory.CreateDirectory(/*Directory.GetCurrentDirectory() +*/ parentDir + imageDir);
            }

            //File.AppendAllText("c:\\text.txt", /*Directory.GetCurrentDirectory() +*/  imagePath + "\r\n");
            try
            {
                File.WriteAllBytes(/*Directory.GetCurrentDirectory() +*/ parentDir + imagePath, imageData);
            }
            catch (Exception ex)
            {
                //File.AppendAllText("c:\\text.txt", "\r\n" + ex.Message);
            }

            var tourist = _touristRepository.GetFilteredElements(tourist1 => tourist1.UserName == UserName).FirstOrDefault();
            if (tourist != null)
            {
                try
                {
                    _photoRepository.Add(new UploadedPhoto() { TouristID = tourist.ID, hotspotID = hotSpotID, ImageUrl = imagePath.Replace('\\', '/') });
                    _photoRepository.UnitOfWork.Commit();
                    errorMessage = string.Empty;
                    return true;
                }
                catch (InvalidOperationException ioex)
                {
                    errorMessage = ioex.Message;
                    return false;
                }
            }
            else
            {
                errorMessage = "No User Is Existed For User Name " + UserName;
                return false;
            }
        }
    }
}
