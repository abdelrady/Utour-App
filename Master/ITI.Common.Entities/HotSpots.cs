using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.Entities
{
    /*
     * {"hotspots": [{
   "id": "test_1",
   "anchor": { "geolocation": { "lat": 52.3729, "lon": 4.93 } },  
   "text": {
     "title": "The Layar Office", 
     "description": "The Location of the Layar Office", 
     "footnote": "Powered by Layar" },
   "imageURL": "http:\/\/custom.layar.nl\/layarimage.jpeg",
   "actions": [{
     "label": "Contact Layar",
     "uri": "http:\/\/layar.com\/company\/contact\/",
     "autoTriggerRange": 5000,
     "autoTriggerOnly": true,
     "contentType": "text\/plain",
     "method": "GET",
     "activityType": 1,
     "params": [
       "lat",
       "lon"
     ],
     "closeBiw": false,
     "showActivity": true,
     "activityMessage": "contact layar"
    }]
 }], 
 "layer": "snowy4",
 "errorString": "ok", 
 "errorCode": 0
} 
     * 
     * */

    enum BiwStyles { classic, collapsed }
    public class HotSpots
    {
        /// <summary>
        /// used to uniquely identify 
        /// Ancient Anc_
        ///Islamic Isl_
        ///Jewish Jew_
        ///Modern Mod_
        /// Coptic Cop_
        /// </summary>
        public string id;

        public Anchor anchor;
        public Text text { set; get; }

        public Actions[] actions { set; get; }

        /// <summary>
        /// 100x75 pixels,  smaller than 100kb
        /// </summary>
        public string imageURL;

        /// <summary>
        /// Geo Layer Only: decides whether a small BIW dialog should be shown , Default value:	true
        /// </summary>
        public bool showSmallBiw;


        /// <summary>
        /// Geo Layer Only: decides whether a detailed BIW dialog together with actions list should be shown , Default value:	true
        /// </summary>
        public bool showBiwOnClick;

        /// <summary>
        /// Geo Layer Only: specifies the BIW display style.
        /// </summary>
        public string biwStyle;
        //public BiwStyles biwStyle;

        /// <summary>
        /// Geo Layer Only: defines the CIW icon representation for a POI in the Camera view.
        /// </summary>
        //public Icon icon;

        /// <summary>
        /// Uses a 2D (image), 3D (model) or AR video object to represent a POI.
        /// </summary>
        public _Object @object;

        /// <summary>
        /// Enhanced control of positioning objects.
        /// </summary>
        public Transform transform;

        /// <summary>
        /// Animations can be applied to a POI.
        /// </summary>
        public Animations animations;

        /// <summary>
        /// Geo Layer Only: specifies whether a POI should be locked initially in the AR view.
        /// </summary>
        public bool inFocus;

    }
}
