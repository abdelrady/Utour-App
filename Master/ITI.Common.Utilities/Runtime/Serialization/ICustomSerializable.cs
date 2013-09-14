using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.Utilities.IO;

namespace ITI.Common.Utilities.Runtime.Serialization
{
    public interface ICustomSerializable
    {
        void Serialize(ExtendedBinaryWriter writer);
        void Deserialize(ExtendedBinaryReader reader);
    }
}
