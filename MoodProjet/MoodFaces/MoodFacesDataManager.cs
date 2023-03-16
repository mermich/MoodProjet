using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MoodProjet.MoodFaces
{
	public static class MoodFacesDataManager
	{
		public static MySqlConnection GetConn()
		{
			return new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));
		}

		public static void SaveMoodFace(MoodFace moodFace)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"INSERT INTO MoodFace (`Key`, Picture, Label, IsActive) VALUES (@Key, @Picture, @Label, @IsActive);", conn);
			_ = c.Parameters.AddWithValue("@Key", moodFace.Key);
			_ = c.Parameters.AddWithValue("@Picture", moodFace.Picture);
			_ = c.Parameters.AddWithValue("@Label", moodFace.Label);
			_ = c.Parameters.AddWithValue("@IsActive", moodFace.IsActive);
			_ = c.ExecuteNonQuery();
		}


		public static void UpdateMoodFace(MoodFace moodFace)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"UPDATE MoodFace SET `Key` = @Key, Picture = @Picture, Label = @Label, IsActive = @IsActive WHERE Id = @Id;", conn);
			_ = c.Parameters.AddWithValue("@Key", moodFace.Key);
			_ = c.Parameters.AddWithValue("@Picture", moodFace.Picture);
			_ = c.Parameters.AddWithValue("@Label", moodFace.Label);
			_ = c.Parameters.AddWithValue("@IsActive", moodFace.IsActive);
			_ = c.Parameters.AddWithValue("@Id", moodFace.Id);
			_ = c.ExecuteNonQuery();
		}

		public static void DeleteMoodFace(int moodFaceId)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"DELETE FROM MoodFace WHERE Id = @Id;", conn);
			_ = c.Parameters.AddWithValue("@Id", moodFaceId);
			_ = c.ExecuteNonQuery();
		}

		public static List<MoodFace> ListMoodFaces()
		{
			List<MoodFace> result = new();

			using (MySqlConnection conn = GetConn())
			{
				conn.Open();
				MySqlCommand c = new("select Id, `Key`, Picture, Label, IsActive  from MoodFace", conn);
				using MySqlDataReader reader = c.ExecuteReader();
				while (reader.Read())
				{
					MoodFace moodFace = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4));
					result.Add(moodFace);
				}
			}

			return result;
		}
	}
}