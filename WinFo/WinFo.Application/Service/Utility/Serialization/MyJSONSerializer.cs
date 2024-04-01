using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Utility.Serialization
{
    public class MyJSONSerializer : IMySerializer
    {
        public string Serialize(object data)
        {
            string serializedData = "";

            try
            {
                serializedData = JsonConvert.SerializeObject(data);

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return serializedData;
        }
    }
}
