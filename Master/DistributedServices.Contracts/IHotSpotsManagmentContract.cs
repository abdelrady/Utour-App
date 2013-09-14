using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ITI.Common.HotSpotsInfo;

namespace DistributedServices.Contracts
{
    [ServiceContract]
    public interface IHotSpotsManagmentContract
    {
            //[OperationContract]
            LayerInfo RetrieveSurroundingHotSpots(LayerQueryParam layerQueryParams);

            [OperationContract]
            [AspNetCacheProfile("CacheFor1Minute")]
            [WebGet(UriTemplate = "RetrieveHotSpots?lang={lang}&countryCode={countryCode}&lon={lon}&userId={userId}&developerId={developerId}&developerHash={developerHash}&version={version}&radius={radius}&timestamp={timestamp}&lat={lat}&layerName={layerName}&accuracy={accuracy}", ResponseFormat = WebMessageFormat.Json)]
            LayerInfo RetrieveHotSpots(string lang, string countryCode, string lon, string userId, string developerId, string developerHash, string version, string radius, string timestamp, string lat, string layerName, string accuracy);
    }
}
