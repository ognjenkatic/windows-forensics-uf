using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class RecycleBinEntry
    {
        #region fields
        private string _filePath;
        private DateTime _dateTimeDeleted;
        private long _deletedFIleSize;


        #endregion

        #region properties
        public string FilePath { get => _filePath; set => _filePath = value; }
        public DateTime DateTimeDeleted { get => _dateTimeDeleted; set => _dateTimeDeleted = value; }
        public long DeletedFIleSize { get => _deletedFIleSize; set => _deletedFIleSize = value; }
        #endregion


    }
}
