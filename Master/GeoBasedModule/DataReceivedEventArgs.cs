using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.Entities;

namespace GeoBasedModule
{
    public class DataReceivedEventArgs : EventArgs
    {
        public HotSpots[] POIs { set; get; }
    }

    public class UploadedPhotoEventArgs : EventArgs 
    {
        public bool result { set; get; }
    }
}
