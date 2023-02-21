using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MoodProjet.Charts
{
    public static class ChartsDataManager
    {
        public static MySqlConnection GetConn() => new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));

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

    }
}