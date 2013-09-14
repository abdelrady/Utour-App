using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Application.Contracts;
using DistributedServices.Contracts;
using DistributedServices.UTourService.InstanceProvider;
using ITI.Common.HotSpotsInfo;

namespace DistributedServices.UTourService
{
    // NOTE: You can use the "Rename" command on the "Refractor" menu to change the class name "HotSpotsMgmtService" in code, svc and config file together.
    [UnityInstanceProviderServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple,
        AddressFilterMode = AddressFilterMode.Any)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class HotSpotsMgmtService2 : IHotSpotsManagmentContract
    {
        #region -- Local Types --

        private readonly IHotSpotsManagementService _hotSpotsManagementService;

        public HotSpotsMgmtService2(IHotSpotsManagementService hotSpotsManagementService)
        {
            //if (hotSpotsManagementService == (IHotSpotsManagementService)null)
            //    throw new ArgumentNullException("hotSpotsManagementService");

            this._hotSpotsManagementService = hotSpotsManagementService;
        }

        #endregion

        public LayerInfo RetrieveHotSpots(string lang, string countryCode, string lon, string userId, string developerId, string developerHash, string version, string radius, string timestamp, string lat, string layerName, string accuracy)
        {
            //var d1 = DateTime.Now;

            var layerQueryParams = new LayerQueryParam
                                       {
                accuracy = accuracy,
                lat = lat,
                lon = lon,
                radius = radius,
                countryCode = countryCode,
                developerHash = developerHash,
                developerId = developerId,
                lang = lang,
                layerName = layerName,
                timestamp = timestamp,
                userId = userId,
                version = version
            };
            var layerInfo = _hotSpotsManagementService.RetrieveSurroundingHotSpots(layerQueryParams);
            //var layerInfo = new LayerInfo()
            //                    {
            //                        layer = "test",
            //                        errorCode = "0",
            //                        errorString = "Ok",
            //                        hotspots = new HotSpots[] { new HotSpots() }
            //                    };
            //var ts = DateTime.Now - d1;
            //File.AppendAllText("c:\\serviceTrace.txt" , "\r\n" + ts.Milliseconds.ToString());

            return layerInfo;
        }

        public LayerInfo RetrieveSurroundingHotSpots(LayerQueryParam layerQueryParams)
        {
            throw new NotImplementedException();
        }
    }
}
