using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about commands that are executed at startup
    /// </summary>
    public class StartupEntry
    {
        #region fields
        private string _entryName;
        private string _entryCommand;


        #endregion

        #region properties
        public string EntryName { get => _entryName; set => _entryName = value; }
        public string EntryCommand { get => _entryCommand; set => _entryCommand = value; }
        #endregion

    }
}
