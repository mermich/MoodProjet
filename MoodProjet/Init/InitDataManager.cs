using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MoodProjet.Init
{
    public static class InitDataManager
    {
        public static MySqlConnection GetConn() => new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));


        public static string CheckDbConn()
        {
            try
            {
                using (MySqlConnection conn = GetConn())
                {
                    conn.Open();
                    MySqlCommand c = new MySqlCommand("SELECT 1 FROM dual", conn);
                    c.ExecuteNonQuery();
                }

                return "Connection with the database was ok.";
            }
            catch (Exception)
            {
                return "Something seems wrong with the connection string please check file : launchSettings.json";
            }
        }

        public static string InitDB()
        {
            var initDbFile = Path.Combine(Environment.CurrentDirectory, "Init", "script00.sql");
            if (File.Exists(initDbFile))
            {
                using (MySqlConnection conn = GetConn())
                {
                    try
                    {
                        conn.Open();
                        var script = new MySqlScript(conn, File.ReadAllText(initDbFile));
                        script.Execute();

                        return $"Tables created.";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            else
            {
                return $"Cannot find file with path :{initDbFile}";
            }
        }


        public static string SetConfigurationTables()
        {
            var initDbFile = Path.Combine(Environment.CurrentDirectory, "Init", "script01.sql");
            if (File.Exists(initDbFile))
            {
                using (MySqlConnection conn = GetConn())
                {
                    try
                    {
                        conn.Open();
                        var script = new MySqlScript(conn, File.ReadAllText(initDbFile));
                        script.Execute();

                        return $"Configuration tables configured.";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            else
            {
                return $"Cannot find file with path :{initDbFile}";
            }
        }

        public static string SetEntriesTables()
        {
            var initDbFile = Path.Combine(Environment.CurrentDirectory, "Init", "script02.sql");
            if (File.Exists(initDbFile))
            {
                using (MySqlConnection conn = GetConn())
                {
                    try
                    {
                        conn.Open();
                        var script = new MySqlScript(conn, File.ReadAllText(initDbFile));
                        script.Execute();

                        return $"Rows instered in the entrie table.";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            else
            {
                return $"Cannot find file with path :{initDbFile}";
            }
        }
    }
}