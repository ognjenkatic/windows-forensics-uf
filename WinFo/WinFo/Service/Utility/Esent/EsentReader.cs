using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Vista;

using System.IO;

namespace WinFo.Service.Utility.Esent
{
    public class EsentReader
    {
     
        private string _filePath;

        public EsentReader(string filePath)
        {
            _filePath = filePath;
        }

        private EsentTableRow FetchRow(Table table,Session session, List<ColumnInfo> columnInfos)
        {
            EsentTableRow etr = new EsentTableRow();
            etr.Columns = new EsentTableColumn[columnInfos.Count];

            int rowIndex = 0;
            foreach(ColumnInfo columnInfo in columnInfos)
            {
                EsentTableColumn etc = new EsentTableColumn();
                switch (columnInfo.Coltyp)
                {
                    case (JET_coltyp.DateTime):
                        {
                            etc.Type = typeof(DateTime);
                            etc.Name = columnInfo.Name;
                            etc.Value = Api.RetrieveColumnAsDateTime(session, table, columnInfo.Columnid);
                            break;
                        }
                    case (JET_coltyp.Long):
                        {
                            etc.Type = typeof(Int32);
                            etc.Name = columnInfo.Name;
                            etc.Value = Api.RetrieveColumnAsInt32(session, table, columnInfo.Columnid);
                            break;
                        }
                    //To-DO: fix this ugly workaround, enum seems to be missing values
                    default:
                        {
                            etc.Type = typeof(Int64);
                            etc.Name = columnInfo.Name;
                            etc.Value = Api.RetrieveColumnAsInt64(session, table, columnInfo.Columnid);
                            break;
                        }
                }

                etr.Columns[rowIndex++] = etc;
            }
            return etr;
        }

        public List<EsentTableRow> GetRows(string tableName)
        {
            List<EsentTableRow> rows = new List<EsentTableRow>();

            using (Instance m_jetInstance = new Instance("ESEVIEW"))
            {
                m_jetInstance.Init();
                using (Session m_sesid = new Session(m_jetInstance))
                {
                    JET_DBID m_dbid = JET_DBID.Nil;
                    Api.JetAttachDatabase(m_sesid, _filePath, AttachDatabaseGrbit.ReadOnly);
                    Api.JetOpenDatabase(m_sesid, _filePath, null, out m_dbid, OpenDatabaseGrbit.ReadOnly);
                    
                    using (Table table = new Table(m_sesid, m_dbid, tableName, OpenTableGrbit.ReadOnly))
                    {
                        var columns = new List<ColumnInfo>(Api.GetTableColumns(m_sesid, table));

                        while (Api.TryMoveNext(m_sesid, table))
                        {
                            EsentTableRow etr = FetchRow(table, m_sesid, columns);
                            rows.Add(etr);
                        }

                        Api.JetCloseDatabase(m_sesid, m_dbid, CloseDatabaseGrbit.None);
                        Api.JetDetachDatabase(m_sesid, tableName);

                        Console.WriteLine("da");
                    }
                };
                m_jetInstance.Term();
            };
            return rows;
        }
    }
}
