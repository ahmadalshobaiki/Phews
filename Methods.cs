using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Phews
{
    public static class Methods
    {
        static public bool isLoggedIn()
        {
            if (HttpContext.Current.Session["ID"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool isAdmin()
        {
            ArrayList roleList = (ArrayList)HttpContext.Current.Session["role"];
            foreach (string role in roleList)
            {
                if (role.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool isSuperAdmin()
        {
            ArrayList roleList = (ArrayList)HttpContext.Current.Session["role"];
            foreach (string role in roleList)
            {
                if (role.Equals("super Admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool isUserP()
        {
            ArrayList roleList = (ArrayList)HttpContext.Current.Session["role"];
            foreach (string role in roleList)
            {
                if (role.Equals("userp", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool isUserN()
        {
            ArrayList roleList = (ArrayList)HttpContext.Current.Session["role"];
            foreach (string role in roleList)
            {
                if (role.Equals("usern", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static DataTable StoredProceduredt(string storedProcedure, ArrayList values)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < values.Count; i += 2)
            {
                cmd.Parameters.AddWithValue(values[i].ToString(), values[i + 1].ToString());
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return dt;
        }

        public static void StoredProcedure(string storedProcedure, ArrayList values)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand(storedProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < values.Count; i += 2)
            {
                cmd.Parameters.AddWithValue(values[i].ToString(), values[i + 1]);
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
        }
    }
}