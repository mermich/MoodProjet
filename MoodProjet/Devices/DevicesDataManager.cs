using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MoodProjet.Devices
{
	public static class DevicesDataManager
	{
		public static MySqlConnection GetConn()
		{
			return new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));
		}

		public static void SaveDevice(Device device)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"INSERT INTO Device (Label, IsActive) VALUES (@Label, @IsActive);", conn);
			_ = c.Parameters.AddWithValue("@Label", device.Label);
			_ = c.Parameters.AddWithValue("@IsActive", device.IsActive);
			_ = c.ExecuteNonQuery();
		}


		public static void UpdateDevice(Device device)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"UPDATE Device SET Label = @Label, IsActive = @IsActive WHERE Id = @Id;", conn);
			_ = c.Parameters.AddWithValue("@Label", device.Label);
			_ = c.Parameters.AddWithValue("@IsActive", device.IsActive);
			_ = c.Parameters.AddWithValue("@Id", device.Id);
			_ = c.ExecuteNonQuery();
		}

		public static void DeleteDevice(int deviceId)
		{
			using MySqlConnection conn = GetConn();
			conn.Open();
			MySqlCommand c = new($"DELETE FROM Device WHERE Id = @id;", conn);
			_ = c.Parameters.AddWithValue("@Id", deviceId);
			_ = c.ExecuteNonQuery();
		}

		public static List<Device> ListDevices()
		{
			List<Device> result = new();

			using (MySqlConnection conn = GetConn())
			{
				conn.Open();
				MySqlCommand c = new("select Id, Label, IsActive  from Device", conn);
				using MySqlDataReader reader = c.ExecuteReader();
				while (reader.Read())
				{
					Device Device = new(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
					result.Add(Device);
				}
			}

			return result;
		}
	}
}