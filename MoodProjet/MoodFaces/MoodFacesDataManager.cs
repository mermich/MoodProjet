using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MoodProjet.MoodFaces
{
    public static class MoodFacesDataManager
    {
        public static MySqlConnection GetConn() => new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));

        public static void SaveMoodFace(MoodFace moodFace)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"INSERT INTO MoodFace (`Key`, Picture, Label, IsActive) VALUES (@Key, @Picture, @Label, @IsActive);", conn);
                c.Parameters.AddWithValue("@Key", moodFace.Key);
                c.Parameters.AddWithValue("@Picture", moodFace.Picture);
                c.Parameters.AddWithValue("@Label", moodFace.Label);
                c.Parameters.AddWithValue("@IsActive", moodFace.IsActive);
                c.ExecuteNonQuery();
            }
        }


        public static void UpdateMoodFace(MoodFace moodFace)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"UPDATE MoodFace SET `Key` = @Key, Picture = @Picture, Label = @Label, IsActive = @IsActive WHERE Id = @Id;", conn);
                c.Parameters.AddWithValue("@Key", moodFace.Key);
                c.Parameters.AddWithValue("@Picture", moodFace.Picture);
                c.Parameters.AddWithValue("@Label", moodFace.Label);
                c.Parameters.AddWithValue("@IsActive", moodFace.IsActive);
                c.Parameters.AddWithValue("@Id", moodFace.Id);
                c.ExecuteNonQuery();
            }
        }

        public static void DeleteMoodFace(int moodFaceId)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"DELETE FROM MoodFace WHERE Id = @Id;", conn);
                c.Parameters.AddWithValue("@Id", moodFaceId);
                c.ExecuteNonQuery();
            }
        }

        public static List<MoodFace> ListMoodFaces()
        {
            List<MoodFace> result = new List<MoodFace>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Id, `Key`, Picture, Label, IsActive  from MoodFace", conn);
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
    }
}