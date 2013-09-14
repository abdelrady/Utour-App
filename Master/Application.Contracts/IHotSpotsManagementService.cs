using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.HotSpotsInfo;

namespace Application.Contracts
{
    public interface IHotSpotsManagementService
    {
        /// <summary>
        /// Used to retrieve Layer Info + Hotspots around the user
        /// </summary>
        /// <returns></returns>
        LayerInfo RetrieveSurroundingHotSpots(LayerQueryParam layerQueryParams);

    }
    
}
