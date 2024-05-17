using System; 
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Data.SQLite;
using System.IO;
namespace EL
{
    public class DBAccess
    {
        private string connectionString =   StaticCache.DBInfoEntity.SQLConnString;
        private int commandTimeout = StaticCache.DBInfoEntity.CommandTimeout;
        private SQLiteConnection connection;
        private string serverFilename = StaticCache.DBInfoEntity.serverFilename;
        public DBAccess()
        { 
            connection = new SQLiteConnection(connectionString); 
            if (!File.Exists(serverFilename))
            { 
                WriteLog(null, "No database is detected.");  
            }
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            try
            {
                if (!File.Exists(serverFilename))
                {
                    SQLiteConnection.CreateFile(serverFilename);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex,"Failed to create serverFile Named with : " + serverFilename);
                throw ex;
            }
        }
        private void DeleteDatabase()
        {
            try
            {
                if (File.Exists(serverFilename))
                {
                    File.Delete(serverFilename);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, "Failed to delete serverFileName : " + serverFilename);
                throw ex;
            }
        }
        private DataTable LiteSelectDatatable(string QueryCmd, params SQLiteParameter[] para)
        {
            DataTable dataTable = new DataTable();
            try
            { 
                //string query = "SELECT * FROM Q1";
                using (var adapt = new SQLiteDataAdapter(QueryCmd, connection))
                {
                    connection.Open();
                    adapt.SelectCommand.CommandTimeout = commandTimeout;
                    adapt.SelectCommand.CommandType = CommandType.Text;

                    if (para != null)
                    {
                        para = LiteChangeToDBNull(para);
                        adapt.SelectCommand.Parameters.AddRange(para);
                    }

                    adapt.Fill(dataTable);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, QueryCmd);
                throw ex;
            }
            return dataTable;
        }
        public bool LiteInsertUpdateDeleteData(string sSQL, bool useOptimisticExclusion, params SQLiteParameter[] para)
        {
            try
            { 
                using (var cmd = new SQLiteCommand(sSQL, connection))
                {
                    connection.Open();
                    cmd.CommandTimeout = commandTimeout;
                    cmd.CommandType = CommandType.Text;

                    if (para != null)
                    {
                        para = LiteChangeToDBNull(para);
                        cmd.Parameters.AddRange(para);
                        if (useOptimisticExclusion)
                        {
                            cmd.Parameters.Add(new SQLiteParameter("@OutExclusionError", SqlDbType.TinyInt)
                            {
                                Direction = ParameterDirection.Output
                            });
                        }
                    }

                    var transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    try
                    {
                        int ret = cmd.ExecuteNonQuery();
                        if (useOptimisticExclusion)
                        {
                            if (cmd.Parameters["@OutExclusionError"].Value.ToInt16(0) > 0)
                                throw new ExclusionException();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    connection.Close();
                }
                return true;
            }
            catch (ExclusionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
                throw ex;
            }
        }
        public DBAccess(int commandTimeout)
        {
            this.commandTimeout = commandTimeout;
        }

        public void SetCommandTimeout(int value)
        {
            commandTimeout = value;
        }

        public void ResetCommandTimeout()
        {
            commandTimeout = StaticCache.DBInfoEntity.CommandTimeout;
        } 

        public DataSet SelectDataSet(string sSQL, params SqlParameter[] para)
        {
            DataSet dt = new DataSet();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var adapt = new SqlDataAdapter(sSQL, conn))
                {
                    conn.Open();
                    adapt.SelectCommand.CommandTimeout = commandTimeout;
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (para != null)
                    {
                        para = ChangeToDBNull(para);
                        adapt.SelectCommand.Parameters.AddRange(para);
                    }

                    adapt.Fill(dt);
                    conn.Close();
                }

                return dt;
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
                throw ex;
            }
        }

        public string InsertUpdateDeleteSelectData(string sSQL, bool useOptimisticExclusion, params SqlParameter[] para)
        {
            string return_value = string.Empty;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sSQL, conn))
                {
                    conn.Open();
                    cmd.CommandTimeout = commandTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (para != null)
                    {
                        para = ChangeToDBNull(para);
                        cmd.Parameters.AddRange(para);
                        if (useOptimisticExclusion)
                        {
                            cmd.Parameters.Add(new SqlParameter("@OutExclusionError", SqlDbType.TinyInt)
                            {
                                Direction = ParameterDirection.Output
                            });
                        }
                    }

                    var transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;
                    try
                    {
                        //int ret = cmd.ExecuteNonQuery();

                        return_value = (string)cmd.ExecuteScalar();
                        if (string.IsNullOrEmpty(return_value))
                        {
                            if (cmd.Parameters["@OutExclusionError"].Value.ToInt16(0) > 0)
                                throw new ExclusionException();
                        }
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        return_value = string.Empty;
                        transaction.Rollback();
                        throw ex;
                    }
                    //conn.Close();
                }
                return return_value;
            }
            catch (ExclusionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
                throw ex;
            }
        }
        private SQLiteParameter[] LiteChangeToDBNull(SQLiteParameter[] para)
        {
            
            foreach (var p in para)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value; 
                }
                else if (p.DbType ==  DbType.VarNumeric)
                {
                    if (string.IsNullOrWhiteSpace(p.Value.ToString()))
                    {
                        p.Value = DBNull.Value;
                      //  p.SqlValue = DBNull.Value;
                    }
                    else
                    {
                        p.Value = p.Value.ToString().Replace("\t", string.Empty);
                    }
                }
            }
            return para;
        }
        private SqlParameter[] ChangeToDBNull(SqlParameter[] para)
        {
            foreach (var p in para)
            {
                if (p.Value == null)
                {
                    p.Value = DBNull.Value;
                    p.SqlValue = DBNull.Value;
                }
                else if (p.SqlDbType == SqlDbType.VarChar)
                {
                    if (string.IsNullOrWhiteSpace(p.Value.ToString()))
                    {
                        p.Value = DBNull.Value;
                        p.SqlValue = DBNull.Value;
                    }
                    else
                    {
                        p.Value = p.Value.ToString().Replace("\t", string.Empty);
                    }
                }
            }
            return para;
        }

        private void WriteLog(Exception ex, string sql)
        {
           Logger.GetInstance().Error(ex, sql);
        }
    }
}
