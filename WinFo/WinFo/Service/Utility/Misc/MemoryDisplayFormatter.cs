using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.Misc
{
    //TO-DO make this into a value converter if it will only be used for display purposes
    public class MemoryDisplayFormatter
    {
        public static string Format(ulong memoryValue)
        {
            string stringMemoryValue = "";

            double basicUnit = 1024;

           
            if (memoryValue < Math.Pow(basicUnit,2))
                stringMemoryValue = (memoryValue / basicUnit).ToString("0.##") + "KB";
            else if (memoryValue < Math.Pow(basicUnit, 3))
                stringMemoryValue = (memoryValue / Math.Pow(basicUnit, 2)).ToString("0.##") + "MB";
            else if (memoryValue < Math.Pow(basicUnit, 4))
                stringMemoryValue = (memoryValue / Math.Pow(basicUnit, 3)).ToString("0.##") + "GB";
            else if (memoryValue < Math.Pow(basicUnit, 5))
                stringMemoryValue = (memoryValue / Math.Pow(basicUnit, 4)).ToString("0.##") + "TB";
            else
                stringMemoryValue = memoryValue.ToString("0.##") + "B";
            return stringMemoryValue;
        }
    }
}
