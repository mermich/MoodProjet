using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MoodProjet.MoodEntries
{
	public static class MoodEntriesDataManager
	{
		public static MySqlConnection GetConn()
		{
			return new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));
		}

		public static void RunCommand(string command)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			Console.WriteLine(command);
			MySqlCommand c = new(command, conn);
			_ = c.ExecuteNonQuery();
		}

		public static List<MoodEntry> ListMoodEntries()
		{
			List<MoodEntry> result = new();

			using (MySqlConnection conn = GetConn())
			{
				conn.Open();
				MySqlCommand c = new("select Id, MoodFaceId, `Date`, MoodDeviceId from MoodEntry", conn);
				using MySqlDataReader reader = c.ExecuteReader();
				while (reader.Read())
				{
					MoodEntry moodEntry = new(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetInt32(3));
					result.Add(moodEntry);
				}
			}

			return result;
		}


		public static void SaveMoodEntry(MoodEntry moodEntry)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new("INSERT INTO MoodEntry (`Date`, MoodDeviceId, MoodFaceId) VALUES (@Date, @MoodDeviceId, @MoodFaceId);", conn);
			_ = c.Parameters.AddWithValue("@Date", moodEntry.Date.ToString("yyyy-MM-dd H:mm:ss"));
			_ = c.Parameters.AddWithValue("@MoodDeviceId", moodEntry.MoodDeviceId);
			_ = c.Parameters.AddWithValue("@MoodFaceId", moodEntry.MoodFaceId);
			_ = c.ExecuteNonQuery();
		}


		public static void UpdateMoodEntry(MoodEntry moodEntry)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new("UPDATE MoodEntry SET `Date` = @Date, MoodDeviceId = @MoodDeviceId, MoodFaceId = @MoodFaceId WHERE Id = @Id;", conn);
			_ = c.Parameters.AddWithValue("@Date", moodEntry.Date.ToString("yyyy-MM-dd H:mm:ss"));
			_ = c.Parameters.AddWithValue("@MoodDeviceId", moodEntry.MoodDeviceId);
			_ = c.Parameters.AddWithValue("@MoodFaceId", moodEntry.MoodFaceId);
			_ = c.Parameters.AddWithValue("@Id", moodEntry.Id);
			_ = c.ExecuteNonQuery();
		}

		public static void DeleteMoodEntry(int id)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new("DELETE FROM MoodEntry WHERE Id= @Id;", conn);
			_ = c.Parameters.AddWithValue("@Id", id);
			_ = c.ExecuteNonQuery();
		}
	}
}