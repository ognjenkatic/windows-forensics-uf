using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.Esent
{
    public class EsentTableRow
    {
        private EsentTableColumn[] _columns;

        public EsentTableColumn[] Columns { get => _columns; set => _columns = value; }
    }
}
