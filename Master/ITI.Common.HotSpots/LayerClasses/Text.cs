using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.HotSpotsInfo.LayerClasses
{
    public class Text
    {
        public string title;
        public string description;
        public string footnote;
        public string longdescription;
        /// <summary>
        /// it should be double , but for parsing issues
        /// I've let it as a simple string
        /// </summary>
        public string rate;

        public review[] reviews;
        public Photo[] photos;
        public video[] videos;

    }

    /// <summary>
    /// video class used to represent any video info
    /// for specific hotspot
    /// </summary>
    public class video  
    {
        //I've being used small chars for properties just 
        //for compatibility with layer architecture (Data Contract)
        public string id { get; set; }
        public string description { get; set; }
        public string length { get; set; }
        public string url { get; set; }
        public string contentType { get; set; }
    }

    /// <summary>
    /// video class used to represent any video info
    /// for specific hotspot
    /// </summary>
    public class Photo
    {
        public string id { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string contentType { get; set; }
    }

    public class review
    {
        public string usercomment;
        public string username;
    }
}
