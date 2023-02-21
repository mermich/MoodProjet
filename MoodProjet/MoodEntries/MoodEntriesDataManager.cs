using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MoodProjet.MoodEntries
{
    public static class MoodEntriesDataManager
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

        public static List<MoodEntry> ListMoodEntries()
        {
            List<MoodEntry> result = new List<MoodEntry>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Id, MoodFaceId, `Date`, MoodDeviceId from MoodEntry", conn);
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


        public static void SaveMoodEntry(MoodEntry moodEntry)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("INSERT INTO MoodEntry (`Date`, MoodDeviceId, MoodFaceId) VALUES (@Date, @MoodDeviceId, @MoodFaceId);", conn);
                c.Parameters.AddWithValue("@Date", moodEntry.Date.ToString("yyyy-MM-dd H:mm:ss"));
                c.Parameters.AddWithValue("@MoodDeviceId", moodEntry.MoodDeviceId);
                c.Parameters.AddWithValue("@MoodFaceId", moodEntry.MoodFaceId);
                c.ExecuteNonQuery();
            }
        }


        public static void UpdateMoodEntry(MoodEntry moodEntry)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("UPDATE MoodEntry SET `Date` = @Date, MoodDeviceId = @MoodDeviceId, MoodFaceId = @MoodFaceId WHERE Id = @Id;", conn);
                c.Parameters.AddWithValue("@Date", moodEntry.Date.ToString("yyyy-MM-dd H:mm:ss"));
                c.Parameters.AddWithValue("@MoodDeviceId", moodEntry.MoodDeviceId);
                c.Parameters.AddWithValue("@MoodFaceId", moodEntry.MoodFaceId);
                c.Parameters.AddWithValue("@Id", moodEntry.Id);
                c.ExecuteNonQuery();
            }
        }

        public static void DeleteMoodEntry(int id)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("DELETE FROM MoodEntry WHERE Id= @Id;", conn);
                c.Parameters.AddWithValue("@Id", id);
                c.ExecuteNonQuery();
            }
        }
    }
}