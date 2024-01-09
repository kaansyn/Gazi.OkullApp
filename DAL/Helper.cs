using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class HelperException : Exception
{
    public HelperException(string message) : base(message)
    {
    }

    public HelperException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class Helper : IDisposable
{
    private SqlConnection cn;
    private SqlCommand cmd;
    private string cstr = ConfigurationManager.ConnectionStrings["cstr"].ConnectionString;

    public int ExecuteNonQuery(string cmdtext, SqlParameter[] p = null)
    {
        try
        {
            using (cn = new SqlConnection(cstr))
            {
                using (cmd = new SqlCommand(cmdtext, cn))
                {
                    if (p != null)
                    {
                        cmd.Parameters.AddRange(p);
                    }
                    cn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new HelperException("Helper sınıfında bir hata oluştu.", ex);
        }
    }

    public SqlDataReader ExecuteReader(string cmdtext, SqlParameter[] p = null)
    {
        try
        {
            cn = new SqlConnection(cstr);
            cmd = new SqlCommand(cmdtext, cn);
            if (p != null)
            {
                cmd.Parameters.AddRange(p);
            }
            cn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            throw new HelperException("Helper sınıfında bir hata oluştu.", ex);
        }
    }

    public void Dispose()
    {
        try
        {
            if (cn != null && cn.State != ConnectionState.Closed)
            {
                cn.Close();
                cn.Dispose();
                cmd.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw new HelperException("Helper sınıfında bir hata oluştu.", ex);
        }
    }
}
