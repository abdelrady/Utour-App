using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Application.Contracts;
using DistributedServices.Contracts;
using DistributedServices.UTourService.InstanceProvider;
using ITI.Common.HotSpotsInfo;

namespace DistributedServices.UTourService
{
    // NOTE: You can use the "Rename" command on the "Refractor" menu to change the class name "HotSpotsMgmtService" in code, svc and config file together.
    [UnityInstanceProviderServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any )]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class HotSpotsMgmtService : IHotSpotsManagmentContract
    {
        #region -- Local Types --

        private IHotSpotsManagementService hotSpotsManagementService;

        public HotSpotsMgmtService(IHotSpotsManagementService hotSpotsManagementService)
        {
            //if (hotSpotsManagementService == (IHotSpotsManagementService)null)
            //    throw new ArgumentNullException("hotSpotsManagementService");

            this.hotSpotsManagementService = hotSpotsManagementService;
        }

        #endregion

        public LayerInfo RetrieveSurroundingHotSpots(LayerQueryParam layerQueryParams)
        {
            return hotSpotsManagementService.RetrieveSurroundingHotSpots(layerQueryParams);
        }

        [WebGet(UriTemplate = "RetrieveHotSpots?lang={lang}&countryCode={countryCode}&lon={lon}&userId={userId}&developerId={developerId}&developerHash={developerHash}&version={version}&radius={radius}&timestamp={timestamp}&lat={lat}&layerName={layerName}&accuracy={accuracy}", ResponseFormat = WebMessageFormat.Json)]
        public LayerInfo RetrieveHotSpots(string lang, string countryCode, string lon, string userId, string developerId, string developerHash, string version, string radius, string timestamp, string lat, string layerName, string accuracy)
        {
            LayerQueryParam layerQueryParams = new LayerQueryParam()
                                                   {
                                                       accuracy = accuracy,
                                                       lat = lat,
                                                       lon = lon,
                                                       radius = radius,
                                                       countryCode = countryCode,
                                                       developerHash = developerHash,
                                                       developerId = developerId,
                                                       lang = lang,
                                                       layerName=layerName,
                                                       timestamp = timestamp,
                                                       userId=userId,
                                                       version = version
                                                   };
            return hotSpotsManagementService.RetrieveSurroundingHotSpots(layerQueryParams);
        }
    }
}
