using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinfromsChat.Models;

namespace WinfromsChat.DAO
{
    public class UserDAO
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public bool AddUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Users([Login], [Password]) VALUES(@Login, @Password);", connection);
                    SqlParameter loginParam = new SqlParameter("@Login", user.Login);
                    SqlParameter passParam = new SqlParameter("@Password", user.Password);
                    command.Parameters.Add(loginParam);
                    command.Parameters.Add(passParam);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("User with current login is exist.");
            }
            return false;
        }

        public void DeleteUser(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Users WHERE [Login]=@Login;", connection);
                    SqlParameter loginParam = new SqlParameter("@Login", login);
                    command.Parameters.Add(loginParam);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public User GetUserByLogin(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Users WHERE [Login]=@login;", connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return new User(reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT*FROM Users WHERE [Login]=@login;", connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            return new User(reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Users;", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<User> users = new List<User>();
                            while (reader.Read())
                            {
                                users.Add(new User(reader.GetString(0), reader.GetString(1)));
                            }
                            return users;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public User UserAuthorization(string login, string password)
        {
            User user = new User(login, password);
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Users WHERE [Login]=@login AND [Password]=@pass;", connection);
                    command.Parameters.Add(new SqlParameter("@login", user.Login));
                    command.Parameters.Add(new SqlParameter("@pass", user.Password));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return new User(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Invaid login or password");
            }
            return null;
        }
    }
}
