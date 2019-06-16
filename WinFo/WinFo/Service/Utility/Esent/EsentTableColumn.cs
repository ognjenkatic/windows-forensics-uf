using Microsoft.Isam.Esent.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.Esent
{
    public class EsentTableColumn
    {
        private Type _type;
        private string _name;
        private object _value;

        public Type Type { get => _type; set => _type = value; }
        public string Name { get => _name; set => _name = value; }
        public object Value { get => _value; set => _value = value; }
    }
}
