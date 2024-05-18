using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinfromsChat.Models;

namespace WinfromsChat.DAO
{
    public class ChatDAO
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        private readonly UserDAO _userDAO = new UserDAO();

        public void AddChat(Chat chat)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Chats VALUES(@chatName, @admin, @user)", connection);
                    command.Parameters.Add(new SqlParameter("@chatName", chat.ChatName));
                    command.Parameters.Add(new SqlParameter("@admin", chat.UserAdmin.Login));
                    command.Parameters.Add(new SqlParameter("@user", chat._User.Login));
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

        public List<Chat> GetChatsByAdmin(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Chats WHERE Chats.LoginUserAdmin=@login;";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Chat> chats = new List<Chat>();
                            while (reader.Read())
                            {
                                User admin = _userDAO.GetUserByLogin(reader.GetString(2));
                                User user = _userDAO.GetUserByLogin(reader.GetString(3));
                                Chat chat = new Chat(admin, user, reader.GetString(1));
                                chat.Id = reader.GetInt32(0);
                                chats.Add(chat);
                            }
                            return chats;
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

        public List<Chat> GetChatsByUser(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Chats WHERE Chats.LoginUser=@login;";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Chat> chats = new List<Chat>();
                            while (reader.Read())
                            {
                                User admin = _userDAO.GetUserByLogin(reader.GetString(2));
                                User user = _userDAO.GetUserByLogin(reader.GetString(3));
                                Chat chat = new Chat(admin, user, reader.GetString(1));
                                chat.Id = reader.GetInt32(0);
                                chats.Add(chat);
                            }
                            return chats;
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

        public List<Chat> GetChatsByLogin(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Chats WHERE LoginUserAdmin=@login OR LoginUser=@login;", connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Chat> chats = new List<Chat>();
                            while (reader.Read())
                            {
                                User userAdmin = _userDAO.GetUserByLogin(reader.GetString(2));
                                User user = _userDAO.GetUserByLogin(reader.GetString(3));
                                chats.Add(new Chat(userAdmin, user, reader.GetString(1)) { Id = reader.GetInt32(0) });
                            }
                            return chats;
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

        public async Task<List<Chat>> GetChatsByLoginAsync(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT*FROM Chats WHERE LoginUserAdmin=@login OR LoginUser=@login;", connection);
                    command.Parameters.Add(new SqlParameter("@login", login));
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            List<Chat> chats = new List<Chat>();
                            while (await reader.ReadAsync())
                            {
                                User userAdmin = await _userDAO.GetUserByLoginAsync(reader.GetString(2));
                                User user = await _userDAO.GetUserByLoginAsync(reader.GetString(3));
                                chats.Add(new Chat(userAdmin, user, reader.GetString(1)) { Id = reader.GetInt32(0) });
                            }
                            return chats;
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

        public List<Chat> GetChatsByChatName(string chatName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Chats WHERE ChatName=@chatName;", connection);
                    command.Parameters.Add(new SqlParameter("@chatName", chatName));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Chat> chats = new List<Chat>();
                            while (reader.Read())
                            {
                                User admin = _userDAO.GetUserByLogin(reader.GetString(2));
                                User user = _userDAO.GetUserByLogin(reader.GetString(3));
                                Chat chat = new Chat(admin, user, reader.GetString(1));
                                chat.Id = reader.GetInt32(0);
                                chats.Add(chat);
                            }
                            return chats;
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

        public Chat GetChatById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT*FROM Chats WHERE Id=@id;", connection);
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            User admin = _userDAO.GetUserByLogin(reader.GetString(2));
                            User user = _userDAO.GetUserByLogin(reader.GetString(3));
                            Chat chat = new Chat(admin, user, reader.GetString(1));
                            chat.Id = reader.GetInt32(0);
                            return chat;
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

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT*FROM Chats WHERE Id=@id;", connection);
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            User admin = await _userDAO.GetUserByLoginAsync(reader.GetString(2));
                            User user = await _userDAO.GetUserByLoginAsync(reader.GetString(3));
                            Chat chat = new Chat(admin, user, reader.GetString(1));
                            chat.Id = reader.GetInt32(0);
                            return chat;
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
    }
}
