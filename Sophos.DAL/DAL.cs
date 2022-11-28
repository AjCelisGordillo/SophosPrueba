using System;
using System.Data;
using System.Data.SqlClient;

namespace Sophos.DAL
{
    public class DAL
    {
        SqlConnectionStringBuilder ConnectionBuilder = new SqlConnectionStringBuilder();

        public DAL()
        {
            ConnectionBuilder.DataSource = AppSettings.DataSource;
            ConnectionBuilder.UserID = AppSettings.UserId;
            ConnectionBuilder.Password = AppSettings.Password;
            ConnectionBuilder.InitialCatalog = AppSettings.InitialCatalog;
            ConnectionBuilder.IntegratedSecurity = true;
            ConnectionBuilder.MultipleActiveResultSets = true;
        }


        public DataTable GetTable(string procedure)
        {
            try
            {
                DataTable dt = new DataTable(null);

                using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetTable(string procedure, params object[] parameters)
        {
            try
            {
                DataTable dt = new DataTable(null);

                using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;


                        cnn.Open();
                        SqlCommandBuilder.DeriveParameters(cmd);
                        cmd.Parameters.RemoveAt(0);

                        for (int i = 0; i < cmd.Parameters.Count; i++)
                        {
                            cmd.Parameters[i].TypeName = string.Empty;
                            cmd.Parameters[i].Value = parameters[i];
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool SaveUpdateDelete(string procedure, params object[] parameters)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;

                        cnn.Open();
                        SqlCommandBuilder.DeriveParameters(cmd);
                        cmd.Parameters.RemoveAt(0);

                        for (int i = 0; i < cmd.Parameters.Count; i++)
                        {
                            cmd.Parameters[i].TypeName = string.Empty;
                            cmd.Parameters[i].Value = parameters[i];
                        }

                        var RowsAffected = cmd.ExecuteNonQuery();
                        return RowsAffected >= 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
