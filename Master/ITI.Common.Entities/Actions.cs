using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.Entities
{
    /// <summary>
    /// [Deprecated] convert it to enum.
    /// </summary>
    public static class MimeTypes
    {
        //Type video
        public static string video_3gp = "video/3gpp";
        public static string video_mpeg = "video/mpeg";
        public static string video_mp4 = "video/mp4";
        public static string video_ogg = "video/ogg";
        public static string video_quicktime = "video/quicktime";
        public static string video_webm = "video/webm";
        public static string video_x_matroska = "video/x-matroska";
        public static string video_x_ms_wmv = "video/x-ms-wmv";
        public static string video_x_flv = "video/x-flv";

        //Type text
        public static string text_html = "text/html";
        public static string text_plain = "text/plain";
        public static string video_xml = "text/xml";

        //Type image
        public static string image_gif = "image/gif";
        public static string image_jpeg = "image/jpeg";
        public static string image_png = "image/png";
        public static string image_tiff = "image/tiff";

        //Type application
        public static string application_atom_xml = "application/atom+xml";
        public static string application_json = "application/json";
        public static string application_javascript = "application/javascript";
        //layar Specific
        public static string application_vnd_layar_internal = "application/vnd.layar.internal";
        public static string application_vnd_layar_async = "application/vnd.layar.async";

        
        //Type audio
        public static string audio_mp4 = "audio/mp4";
        public static string audio_mpeg = "audio/mpeg";
        public static string audio_ogg = "audio/ogg";
        public static string audio_vorbis = "audio/vorbis";
        public static string audio_webm = "audio/webm";


    }
    public enum ActionMethods { GET, POST }
    public enum ActionParams { lat, lon, alt, lang, countrycode, localCountryCode, version }
    public enum ActivityType 
    {
        Info=1,
        Audio,
        Video,
        Phone_Call,
        Email_Message,
        Position_Map_Navigate,
        Add,
        Remove,
        Edit,
        Collect,
        Play_Song,
        Play_In_Game,
        Share_Post_Publish,
        Pin_Mark_On_Map,
        Check_In_Out,
        Log_In_Out,
        Lock_Unlock,
        List_Scores,
        Money_Pay_Buy,
        Open,
        Close,
        Update,
        Switch_2_Layer,
        Yes,
        Increase_Decrease,
        View_Examine_Details,
        Add_2_Favorites,
        Kill_Hit_Attack,
        Heal,
        Defend,
        Repair,
        Use_Interact_Switch,
        Animate,
        Scoreboard,
        Generic_Action,
        No
    }
    //for more info refer to http://layar.com/documentation/browser/api/getpois-response/actions/
    public class Actions
    {
        
        //
        /// <summary>
        /// MUST , The URL to send the request to.
        /// </summary>
        public string uri { set; get; }

        //
        /// <summary>
        /// MUST , The text displayed in the action button
        /// </summary>
        public string label { set; get; }

        /// <summary>
        /// MUST , The type of document that is being fetched. 
        /// Default value: application/vnd.layar.internal , 
        /// http://en.wikipedia.org/wiki/Mime_type
        /// </summary>
        public string contentType;

        /// <summary>
        /// The request type, GET or POST
        /// </summary>
        public string method;
        //public ActionMethos method;

        // 
        /// <summary>
        /// A list parameters can be added to the request. , note that params NOT @params , but it's reserved in c# 
        /// </summary>
        public string[] @params;
        //public ActionParams[] @params;

        /// <summary>
        /// The icon shown on the action button.
        /// </summary>
        public ActivityType activityType;

        /// <summary>
        /// Indicates whether to show the background activity.
        /// </summary>
        public bool showActivity=true;

        /// <summary>
        /// The message shown while waiting for the activity response.
        /// </summary>
        public string activityMessage;

        /// <summary>
        /// Geo Layer Only: specifies whether BIW should be closed after clicking the action button.
        /// </summary>
        public bool closeBiw = false;

        /// <summary>
        /// Only for Geo POIs, indicates whether or not this action can be autotriggered by reaching the POI at specific Distance.
        /// </summary>
        public int autoTriggerRange = 10;

        /// <summary>
        /// Only for Vision POIs, indicates whether or not this action can be autotriggered by reaching the POI.
        /// </summary>
        public bool autoTrigger = false;

        /// <summary>
        /// Indicates whether or not this action can be invoked manually.
        /// </summary>
        public bool autoTriggerOnly = true;

    }
}
