using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace SallyDataConnectivity
{
    internal interface ISqlClientDataOperations
    {
        IDataReader ExecuteReader(ArrayList parameterList, string StoredProcName);
        IDataReader ExecuteReaderNoParams(string StoredProcName);
        IDataReader ExecuteReader(string SQL);
        IDataAdapter LoadDataSet(ArrayList parameterList, string StoredProcName);
        int ExecuteNonQuery(ArrayList parameterList, string StoredProcName);
        int ExecuteNonQueryNoParams(string StoredProcName);
        int ExecuteNonQuery(string SQL);
    }
}
