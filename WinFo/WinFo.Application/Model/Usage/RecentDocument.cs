using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class RecentDocument
    {
        #region fields
        string _name;
        string _extension;
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public string Extension { get => _extension; set => _extension = value; }
        #endregion
    }
}
