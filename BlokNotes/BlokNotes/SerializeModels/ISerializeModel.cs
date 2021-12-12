using System;
using System.Collections.Generic;
using System.Text;

namespace BlokNotes.SerializeModels
{
    public interface ISerializeModel
    {
        string ConvertToJson();
    }
}
