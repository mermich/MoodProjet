using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MoodProjet.Devices
{
    public static class DevicesDataManager
    {
        public static MySqlConnection GetConn() => new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));

        public static void SaveDevice(Device device)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"INSERT INTO Device (Label, IsActive) VALUES (@Label, @IsActive);", conn);
                c.Parameters.AddWithValue("@Label", device.Label);
                c.Parameters.AddWithValue("@IsActive", device.IsActive);
                c.ExecuteNonQuery();
            }
        }


        public static void UpdateDevice(Device device)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"UPDATE Device SET Label = @Label, IsActive = @IsActive WHERE Id = @Id;", conn);
                c.Parameters.AddWithValue("@Label", device.Label);
                c.Parameters.AddWithValue("@IsActive", device.IsActive);
                c.Parameters.AddWithValue("@Id", device.Id);
                c.ExecuteNonQuery();
            }
        }

        public static void DeleteDevice(int deviceId)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"DELETE FROM Device WHERE Id = @id;", conn);
                c.Parameters.AddWithValue("@Id", deviceId);
                c.ExecuteNonQuery();
            }
        }

        public static List<Device> ListDevices()
        {
            List<Device> result = new List<Device>();

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Id, Label, IsActive  from Device", conn);
                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Device Device = new Device(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                        result.Add(Device);
                    }
                }
            }

            return result;
        }
    }
}