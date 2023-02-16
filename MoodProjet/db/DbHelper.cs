using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace MoodProjet.db
{
    public static class DbHelper
    {
        public static MySqlConnection GetConn() => new MySqlConnection("Server=127.0.0.1;Port=3306;Database=moodDb;Uid=admin;Pwd=admin;");


        public static void RunCommand(string command)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                Console.WriteLine(command);
                MySqlCommand c = new MySqlCommand(command, conn);
                c.ExecuteNonQuery();
            }
        }

        public static List<ChartData> GetChartData()
        {
            List<ChartData> result = new List<ChartData>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                string query = @"SELECT 
COUNT(CASE WHEN MoodFaceId=1 THEN 1 END) as face1, 
COUNT(CASE WHEN MoodFaceId=2 THEN 1 END) as face2, 
COUNT(CASE WHEN MoodFaceId=3 THEN 1 END) as face3,
COUNT(CASE WHEN MoodFaceId=4 THEN 1 END) as face4,
DATE(`Date`) as jour
FROM moodentry
GROUP BY DATE(`Date`)
order by  DATE(`Date`);";

                Console.WriteLine(query);
                MySqlCommand c = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChartData chartData = new ChartData(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetDateTime(4)
                        );

                        result.Add(chartData);
                    }
                }
            }

            return result;
        }


        public static List<MoodFace> ListMoodFaces()
        {
            List<MoodFace> result = new List<MoodFace>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                string query = "select Id, `Key`, Picture, Label, IsActive  from MoodFace";

                Console.WriteLine(query);
                MySqlCommand c = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MoodFace moodFace = new MoodFace(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4));
                        result.Add(moodFace);
                    }
                }
            }

            return result;
        }

        public static List<MoodEntry> ListMoodEntries()
        {
            List<MoodEntry> result = new List<MoodEntry>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                string query = @"select Id, MoodFaceId, `Date`, MoodDeviceId from mooddb.MoodEntry";

                Console.WriteLine(query);
                MySqlCommand c = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MoodEntry moodEntry = new MoodEntry(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetInt32(3));
                        result.Add(moodEntry);
                    }
                }
            }

            return result;
        }



        public static List<Device> ListDevices()
        {
            List<Device> result = new List<Device>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                string query = "select Id, Label, IsActive from Device";

                Console.WriteLine(query);
                MySqlCommand c = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Device device = new Device(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                        result.Add(device);
                    }
                }
            }

            return result;
        }
    }
}