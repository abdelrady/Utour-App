using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.HotSpotsInfo.LayerClasses;

namespace ITI.Common.HotSpotsInfo
{
    public class LayerInfo      //for more info , refer to http://layar.com/documentation/browser/api/getpois-response/
    {
        /// <summary>
        /// A list of POIs returned in the current layer.
        /// </summary>
        
        public HotSpots[] hotspots;
        /// <summary>
        /// The name of the layer for which the POI's are returned.
        /// </summary>
        
        public string layer;

        /// <summary>
        /// If an error condition occurs, this will be notified using the error code.
        /// </summary>
        
        public string errorCode;
        //public ErrorCodes errorCode;//If an error condition occurs, this will be notified using the error code.
        /// <summary>
        /// The error message associated with the error code above.
        /// </summary>
        
        public string errorString;//

        /// <summary>
        /// The key to the page being returned.
        /// </summary>
        
        public string nextPageKey;//
        /// <summary>
        /// indicates whether more pages need to be retrieved.
        /// </summary>
        
        public bool morePages;//

        /// <summary>
        /// Geo Layer Only: specifies the search radius when no radius value is present in the getPOIs request.
        /// </summary>
        
        public int radius;//

        /// <summary>
        /// Indicates how many seconds to wait until the next getPOIs request is made.
        /// </summary>
        
        public int refreshInterval;//
        /// <summary>
        /// Geo Layer Only: indicates the distance threshold until the next getPOIs request is made.
        /// </summary>
        
        public int refreshDistance;//

        /// <summary>
        /// Indicates the type of refresh
        /// </summary>
        
        public bool fullRefresh;//

        /// <summary>
        /// This enables actions on the entire layer level , Geo Layer Only: defines actions on entire layer level.
        /// </summary>
        //
        public Actions[] actions;

        /// <summary>
        /// A pop-up message as a result of a getPOIs request.
        /// </summary>
        
        public string showMessage;//
        /// <summary>
        /// Specifies which POIs should be removed from the view.
        /// </summary>
        
        public HotSpots[] deletedHotspots;//

        //
        //public Dictionary<string , AnimationEvent> animations;
        //public Animation animations;//Defines animations on layer level.

        //
        //public ShowDialog showDialog;//User interaction dialog.

        /// <summary>
        /// Geo Layer Only: specifies the BW style in the Camera view on layer level.
        /// </summary>
        
        public string biwStyle;//

        //deprecated in 6.2
        //
        //public bool disableClueMenu;For Vision enabled layer, this can be used to disable the menu that shows thumbnails of trackable images.



        public static LayerInfo GetSamplePOI(string lat , string lon)
        {
            return new LayerInfo()
            {
                layer = "SV_Layer",
                errorCode = "0",
                errorString = "Ok",
                hotspots = new HotSpots[]{
                    new HotSpots(){
                    id="geo_2" ,
                    imageURL = "http://farm4.staticflickr.com/3269/2481315696_e6069359f9_z.jpg",
                    text=new Text{title = "فودافون القريه الذكيه" ,
                        description = "مبني فودافون القريه الذكيه 6 اكتوبر. المسافه: "+//The Location of the vodafone Headquarter
                        string.Format("{0:f2}",MesuringDistanceAlgorithms.GetDistanceBetweenPoints(30.0732721, 31.0177597,double.Parse(lat) , double.Parse(lon))),
                        footnote = "جميع الحقوق محفوظه 2012" },
                    anchor= new Anchor(){geolocation= new GeoLocation(){ lat= "30.0732721" , lon="31.0177597"}}
                    } 
                    , 
                new HotSpots(){
                    id="geo_3" , //30.0719653, 31.0217535
                    imageURL = "http://upload.wikimedia.org/wikipedia/commons/0/0d/Grupa_ITI.JPG",
                    text=new Text{title = "The NTI Building" ,
                        description = "The Location of the national Telecommunication Institute. Distance: "+
                        string.Format("{0:f2}",MesuringDistanceAlgorithms.GetDistanceBetweenPoints(30.0719653 , 31.0217535 ,double.Parse(lat) , double.Parse(lon))),
                        footnote = "powered by ITIANs" },
                    anchor= new Anchor(){geolocation= new GeoLocation(){ lat= "30.0719653" , lon="31.0217535"}} ,
                } ,
                new HotSpots(){
                    id="geo_4" ,//30.071125, 31.0213887
                    imageURL = "http://mw2.google.com/mw-panoramio/photos/medium/42015681.jpg",
                    text=new Text{title = "The ITI Building" ,
                        description = "The Location of the Information Technology Institute. Distance: "+
                        string.Format("{0:f2}",MesuringDistanceAlgorithms.GetDistanceBetweenPoints(30.071125, 31.0213887 ,double.Parse(lat) , double.Parse(lon))),
                        footnote = "powered by ITIANs" },
                    anchor= new Anchor(){geolocation= new GeoLocation(){ lat= "30.071125" , lon="31.0213887"}}
                    ,
                    actions = new Actions[]{ new Actions(){uri="tel:+2011271775433" , label="call" , activityType = ActivityType.Phone_Call , autoTriggerRange = 30 ,
                        autoTriggerOnly = false , contentType = "application/vnd.layar.internal" , method = ActionMethods.POST.ToString() , @params=new string[]{ActionParams.lat.ToString() , ActionParams.lon.ToString()}}}
                }
                }
            };
        }
    }


}
