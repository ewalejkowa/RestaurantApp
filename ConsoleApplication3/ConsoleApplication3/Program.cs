using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.ServiceModel.Configuration;
using System.Configuration;
using BSK2_Service;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.ServiceModel.Description;
using ConsoleApplication3;
using FastMember;
using System.Reflection;
using System.Threading;

namespace BSK2_Serwer
{
    class Serwer : IService1
    {      
        static void Main(string[] args)
        {
            var binding = new NetNamedPipeBinding();
            var host = new ServiceHost(typeof(Serwer));
            binding.ReceiveTimeout = TimeSpan.FromSeconds(300);
            var ep = host.AddServiceEndpoint(typeof(IService1), new WebHttpBinding(), new Uri("http://localhost:1101/bsk"));
            ep.Behaviors.Add(new WebHttpBehavior());         
            host.Open();
            
            Console.WriteLine("Start");
            Console.ReadKey();
            host.Close();
        }

        public string GetData(int value)
        {
            Console.WriteLine("aaa");
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int GetLabel(string login)
        {
            var obj = new Object();
            var obj2 = new Object();
            int id_number;

            string oradb = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Program Files (x86)\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\DATA\\Restaurant.mdf';Integrated Security=True";
            SqlConnection conn = new SqlConnection(oradb);
            string command = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 AND TABLE_NAME = '"+login+"'";
 
            SqlCommand cmd;
            cmd = new SqlCommand(command, conn);

            conn.Open();
            obj = cmd.ExecuteScalar();
            string col_name = obj.ToString();
            command = "SELECT TOP 1 "+col_name+"  FROM " + login + " ORDER BY " + col_name + " DESC";
            SqlCommand cmd2;
            cmd2 = new SqlCommand(command, conn);
            obj = cmd2.ExecuteScalar();

            id_number = Convert.ToInt32(obj);
            return id_number;

        }
       public float averageOrderValue()
        {

            using (var context = new RestaurantEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var q = (from a in context.ZAMOWIENIE select (int)a.CENA).Average();
                        q = (float)q;

                        return (float)Math.Truncate(100 * q) / 100;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return 0;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public System.Data.DataTable GetTable(string name)
        {
            try
            {
                using (var context = new RestaurantEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {

                        try
                        {
                            if (name == "PRACOWNIK")
                            {
                                var q = (from a in context.PRACOWNIK select a).ToList();
                                DataTable dt = new DataTable();                                   
                                dt = ToDataTable<PRACOWNIK>(q);
                                dt.TableName = "nowy";
                                return dt;    
                            }
                            else if (name == "MENU")
                            {
                                var q = (from a in context.MENU select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<MENU>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "REALIZACJA_ZAMOWIENIA")
                            {
                                var q = (from a in context.REALIZACJA_ZAMOWIENIA select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<REALIZACJA_ZAMOWIENIA>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "POZYCJA_MENU")
                            {
                                var q = (from a in context.POZYCJA_MENU select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<POZYCJA_MENU>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "KATEGORIA")
                            {
                                var q = (from a in context.KATEGORIA select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<KATEGORIA>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "STATUS")
                            {
                                var q = (from a in context.STATUS select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<STATUS>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "ZAMOWIENIE")
                            {
                                var q = (from a in context.ZAMOWIENIE select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<ZAMOWIENIE>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                            else if (name == "ELEMENT_ZAMOWIENIA")
                            {
                                var q = (from a in context.ELEMENT_ZAMOWIENIA select a).ToList();
                                DataTable dt = new DataTable();
                                dt = ToDataTable<ELEMENT_ZAMOWIENIA>(q);
                                dt.TableName = "nowy";
                                return dt;
                            }
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
            return null;
        }

        public System.Data.DataTable GetSelectiveTable(string name, string column, string value)
        {
            try
            {
                string oradb = "Data Source=(DESCRIPTION="
                  + "(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521))"
                  + "(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
                  + "User Id=ewa;Password=password";
                OracleConnection conn = new OracleConnection(oradb);
                OracleCommand cmd;
                conn.Open();

                string command = "Select * FROM " + name + " WHERE " + column + " ='" + value + "'";


                cmd = new OracleCommand(command, conn);
                using (OracleDataAdapter sda = new OracleDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        dt.TableName = "selected";
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
            return null;
        }

        public bool Login(string userName)
        {
            try
            {
                if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
                {
                    System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                    return false;
                }
                decimal id = decimal.Parse(userName);
                using (var context = new RestaurantEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                       
                        try
                        {
                            
                            var q = context.PRACOWNIK.Where(p => p.ID_PRACOWNIKA == id);
                            foreach (var post in q)
                            {
                                if (post.ID_PRACOWNIKA != null) return true;
                            }
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
                return false;
            }
        }

        //usuwanie danego wiersza
        public bool delete_row(string name_of_table, string name_of_key, string name_of_column)
        {
            try
            {
                string com = "Delete from " + name_of_table + " where " + name_of_column + " = " + name_of_key;
                using (var context = new RestaurantEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Database.ExecuteSqlCommand(com);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        } 
                    }
                }
                return true;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException e)
            {
                return false;
            }
        }
        //aktualizacja po edit
        public bool update_row(string name_of_table, string name_of_key, string col_key, string val1, string col1, string val2, string col2,
            string val3, string col3, string val4, string col4, string val5, string col5, string val6, string col6, string val7, string col7)
        {

                var obj = new Object();
                string tmp = "";
                string com;
                if (name_of_table == "ZAMOWIENIE")
                {
                    com = "Update " + name_of_table;

                    com += " set  " + col1 + "=" + "'" + val1 + "'";

                    com += "," + col2 + "=" + "" + val2 + "";

                    com += "," + col3 + "=" + "" + val3 + "";

                    com += "," + col4 + "=" + "'" + val4 + "'";

                    com += "," + col5 + "=" + "'" + val5 + "'";

                }
                else
                {
                    com = "Update " + name_of_table;
                    if (val1 != "")
                    {
                        com += " set  " + col1 + "=" + "'" + val1 + "'";
                        if (val2 != "")
                        {
                            com += "," + col2 + "=" + "'" + val2 + "'";
                            if (val3 != "")
                            {
                                com += "," + col3 + "=" + "'" + val3 + "'";
                                if (val4 != "")
                                {
                                    com += "," + col4 + "=" + "'" + val4 + "'";
                                    if (val5 != "")
                                    {
                                        com += "," + col5 + "=" + "'" + val5 + "'";
                                        if (val6 != "")
                                        {
                                            com += "," + col6 + "=" + "'" + val6 + "'";
                                            if (val7 != "")
                                            {
                                                com += "," + col7 + "=" + "'" + tmp + "'";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                com += " where " + col_key + "=" + name_of_key;
                try
                {

                    using (var context = new RestaurantEntities())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlCommand(com);
                                context.SaveChanges();

                                dbContextTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction.Rollback();
                            }
                        }
                    }
                    return true;

                
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException e)
            {
                return false;
            }
        }
        public bool add_row(string name_of_table, string val1, string col1, string val2, string col2,
         string val3, string col3, string val4, string col4, string val5, string col5, string val6, string col6, string val7, string col7, byte[] passWord, string col8)
        {
            var obj = new Object();
            try
            {
                string com;
                string tmp = "";

                com = "Insert into " + name_of_table + "(";
                if (val1 != "")
                {
                    com += col1;
                    if (val2 != "")
                    {
                        com += "," + col2;
                        if (val3 != "")
                        {

                            com += "," + col3;
                            if (val4 != "")
                            {
                                com += "," + col4;
                                if (val5 != "")
                                {
                                    com += "," + col5;
                                    if (val6 != "")
                                    {
                                        com += "," + col6;
                                        if (val7 != "")
                                        {
                                            com += "," + col7;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                com += ") values (";
                if (val1 != "")
                {
                    com += "'" + val1 + "'";
                    if (val2 != "")
                    {
                        com += "," + "'" + val2 + "'";
                        if (val3 != "")
                        {
                            if (val3.Contains("to_date"))
                            {
                                com += "," + "" + val3 + "";
                            }
                            else com += "," + "'" + val3 + "'";
                            if (val4 != "")
                            {
                                com += "," + "'" + val4 + "'";
                                if (val5 != "")
                                {
                                    com += "," + "'" + val5 + "'";
                                    if (val6 != "")
                                    {
                                        com += "," + "'" + val6 + "'";
                                        if (val7 != "")
                                        {
                                            com += "," + "'" + val7 + "'";

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                com += ")";
                using (var context = new RestaurantEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Database.ExecuteSqlCommand(com);
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
                
                return true;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException e)
            {
                return false;
            }

        }
    }
}
