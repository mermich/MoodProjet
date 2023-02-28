using MoodProjet.Charts;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace MoodProjet.Auth
{
    public static class AuthDataManager
    {
        public static MySqlConnection GetConn() => new MySqlConnection(Environment.GetEnvironmentVariable("MyConn"));

        public static UserLoginResult Login(UserLogin userLogin)
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand($"SELECT `Id`, `CanAdminDevices`, `CanAdminMoodFaces`, `CanAdminMoodEntries`, `CanSeeCharts`" +
                    $" FROM `mooddb`.`user`" +
                    $" WHERE `Login`=@Login AND `Password`=@Password", conn);

                c.Parameters.AddWithValue("@Login", userLogin.Login);
                c.Parameters.AddWithValue("@Password", userLogin.Password);

                using (MySqlDataReader reader = c.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        UserLoginResult userLoginResult = new UserLoginResult(
                            reader.GetInt32(0),
                            userLogin.Login,
                            true,
                            reader.GetBoolean(1),
                            reader.GetBoolean(2),
                            reader.GetBoolean(3),
                            reader.GetBoolean(4)
                        );

                        return userLoginResult;
                    }
                }
            }

            return new UserLoginResult(0, userLogin.Login, false, false, false, false, false);
        }
    }
}