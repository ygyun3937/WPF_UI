using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_UI.ViewModel
{
    /// <summary>
    /// sealed : 다른 클래스가 해당 클래스 상속 하지 못하도록 함
    /// </summary>
    public sealed class SQLDBManager
    {
        public enum ExcuteResult { Fail = -2, Success };

        public string ConnectionString = string.Empty;
        public string InitFileName { get; set; }
        public string Address { get; private set; }
        public string Port { get; private set; }
        public string LastException { get; private set; }
        /// <summary>
        /// SqlConnection : using System.Data.SqlClient;
        /// </summary>
        public SqlConnection Connection { get; private set; }

        /// <summary>
        /// ConnectionState : using System.Data;
        /// </summary>
        public bool IsRunning { get { return Connection.State == ConnectionState.Open ? true : false; } }

        private static SQLDBManager instance;
        public static SQLDBManager Instance
        {
            get
            {
                if (instance == null) instance = new SQLDBManager();

                return instance;
            }
        }

        private SqlCommand _SqlCmd = null;

        public SQLDBManager()
        {
            _SqlCmd = new SqlCommand();
        }

        public bool GetConnection()
        {
            try
            {
                if(ConnectionString == string.Empty)
                {
                    SetConnectionString();
                }

                Connection = new SqlConnection(ConnectionString);

                Connection.Open();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage:{1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                return false;

            }

            if(Connection.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int ExecuteNonQuery(string query)
        {
            lock (this)
            {
                return Execute_NonQuery(query);
            }
        }

        private int Execute_NonQuery(string query)
        {
            int result = (int)ExcuteResult.Fail;

            try
            {
                _SqlCmd = new SqlCommand();
                _SqlCmd.Connection = this.Connection;
                _SqlCmd.CommandText = query;
                result = _SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage:{1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return result;
            }
            return result;
        }

        public bool HasRows(string query)
        {
            lock (this)
            {
                SqlDataReader result = ExecuteReader(query);

                return result.HasRows;
            }
        }

        public SqlDataReader ExecuteReaderQuery(string query)
        {
            lock (this)
            {
                SqlDataReader result = ExecuteReader(query);

                return result;
            }
        }

        public DataSet ExecuteDsQuery(DataSet ds, string query)
        {
            ds.Reset();

            lock (this)
            {
                return ExecuteDataAdt(ds, query);
            }
        }

        public DataSet ExecuteProcedure(DataSet ds, string procName, params string[] pValues)
        {
            lock (this)
            {
                return ExecuteProcedureAdt(ds, procName, pValues);
            }
        }

        public void CancelQuery()
        {
            _SqlCmd.Cancel();
        }

        public void Close()
        {
            Connection.Close();
        }

        #region private
        /// <summary>
        /// DllImport:using System.Runtime.InteropServices;
        /// </summary>
        /// <param name="Description"></param>
        /// <param name="ReservedValue"></param>
        /// <returns></returns>
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// MessageBox :using System.
        /// </summary>
        /// <returns></returns>
        private bool CheckConnection()
        {
            bool result = true;

            if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == false)
            {
                this.LastException = "네트워크 연결 끊김";
                MessageBox.Show(this.LastException, "Error");
                result = false;
            }
            else if(this.Connection == null || this.Connection.State ==ConnectionState.Closed)
            {
                result = this.GetConnection();
            }
            return result;
        }

        private void SetConnectionString()
        {

            string addr = "DESKTOP-29PU1UU";
            string svr = "server_DB";
            string user = "test2";
            string pwd = "1";

            string dataSource = string.Format(@"Data Source={0};Database={1};User Id={2};Password={3}", addr, svr, user, pwd);

            this.Address = addr;
            this.ConnectionString = dataSource;

        }

        private SqlDataReader ExecuteReader(string query)
        {
            SqlDataReader result = null;
            try
            {
                _SqlCmd = new SqlCommand();
                _SqlCmd.Connection = this.Connection;
                _SqlCmd.CommandText = query;
                result = _SqlCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage:{1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return result;
            }

            return result;
        }


        private DataSet ExecuteDataAdt(DataSet ds, string query)
        {
            try
            {
                //SqlDataAdapter cmd = new SqlDataAdapter();
                //cmd.SelectCommand = _SqlCmd;
                //cmd.SelectCommand.Connection = this.Connection;
                //cmd.SelectCommand.CommandText = query;
                //cmd.Fill(ds);

                _SqlCmd.Connection = this.Connection;
                _SqlCmd.CommandText = query;

                SqlDataAdapter cmd = new SqlDataAdapter(_SqlCmd);


                //cmd.SelectCommand.Connection = this.Connection;
                //cmd.SelectCommand.CommandText = query;
                cmd.Fill(ds);

                Connection.Close();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage:{1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return null;
            }
            return ds;
        }
        private DataSet ExecuteProcedureAdt(DataSet ds, string query,params string[] values)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = _SqlCmd;
                adapter.SelectCommand.CommandText = query;
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Connection = this.Connection;

                for(int i=0; i<values.Length; ++i)
                {
                    adapter.SelectCommand.Parameters.Add(values[i]);
                }

                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage:{1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return null;
            }
            return ds;
        }
    }

    #endregion private
}
