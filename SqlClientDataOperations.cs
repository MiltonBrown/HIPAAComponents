using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SallyDataConnectivity
{
    public class SqlClientDataOperations : ISqlClientDataOperations
    {
        private string _connectionstring;

        public string ConnectionString
        {
            get
            {
                return _connectionstring;
            }
            set
            {
                _connectionstring = value;
            }
        }

        #region ExecuteNonQuery Implementation
        public int ExecuteNonQuery(string SQL)
        {
            int _executed = 0;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
            SqlCommand _comm = new SqlCommand(SQL, _conn);
            try
            {
                _conn.Open();
                _comm.ExecuteNonQuery();
                _executed = 1;
                _conn.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _comm.Dispose();
            }
            return _executed;
        }

        public int ExecuteNonQuery(ArrayList parameterList,string StoredProcName)
        {
            int _executed = 0;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
            
            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                _comm.CommandTimeout = 1800; //Setting command timeout to 30 minutes for testing purposes.
                for (int paramindex = 0; paramindex < parameterList.Count;paramindex++ )
                {
                    SqlParameter _Param = new SqlParameter();
                    _Param = (SqlParameter)parameterList[paramindex];
                    _comm.Parameters.Add(_Param);
                }
                _comm.ExecuteNonQuery();
                _executed = 1;
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if(_conn.State.ToString().Equals("Open"))
                {
                    _conn.Close();
                }
            }
            return _executed;
        }

        public int ExecuteNonQueryNoParams(string StoredProcName)
        {
            int _executed = 0;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);

            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                _comm.ExecuteNonQuery();
                _executed = 1;
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (_conn.State.ToString().Equals("Open"))
                {
                    _conn.Close();
                }
            }
            return _executed;
        }
        #endregion

        #region ExecuteReader Implementation
        public IDataReader ExecuteReader(string SQL)
        {
            IDataReader _reader;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
            SqlCommand _comm = new SqlCommand(SQL, _conn);
            try
            {
                _conn.Open();
                _reader = _comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            return _reader;
        }

        public IDataReader ExecuteReader(ArrayList parameterList,string StoredProcName)
        {
            IDataReader _reader = null;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);

            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                for (int paramindex = 0; paramindex < parameterList.Count; paramindex++)
                {
                    SqlParameter _Param = new SqlParameter();
                    _Param = (SqlParameter)parameterList[paramindex];
                    _comm.Parameters.Add(_Param);
                }
                _reader = _comm.ExecuteReader(CommandBehavior.CloseConnection);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
            return _reader;
        }

        public IDataReader ExecuteReaderNoParams(string StoredProcName)
        {
            IDataReader _reader = null;
            SqlConnection _conn = new SqlConnection(this.ConnectionString);

            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                _reader = _comm.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return _reader;
        }

        public IDataAdapter LoadDataSet(ArrayList parameterList,string StoredProcName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
             try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                _comm.CommandTimeout = 5000;
                for (int paramindex = 0; paramindex < parameterList.Count; paramindex++)
                {
                    SqlParameter _Param = new SqlParameter();
                    _Param = (SqlParameter)parameterList[paramindex];
                    _comm.Parameters.Add(_Param);
                }
                da.SelectCommand = _comm;
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
            return da;
        }

        public IDataAdapter LoadDataSet(string SQL)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(SQL, _conn);
                _comm.CommandType = System.Data.CommandType.Text;
                _comm.CommandTimeout = 5000;
                da.SelectCommand = _comm;
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return da;
        }

        public IDataAdapter LoadDataSetFromStoredProc(string StoredProcName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection _conn = new SqlConnection(this.ConnectionString);
            try
            {
                _conn.Open();
                SqlCommand _comm = new SqlCommand(StoredProcName, _conn);
                _comm.CommandType = System.Data.CommandType.StoredProcedure;
                _comm.CommandTimeout = 5000;
                da.SelectCommand = _comm;
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return da;
        }
        #endregion
    }
}
