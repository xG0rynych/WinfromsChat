using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinfromsChat.Models;
using System.Windows.Forms;

namespace WinfromsChat.DAO
{
    public class MessageDAO
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        private readonly UserDAO _userDAO = new UserDAO();
        private readonly ChatDAO _chatDAO = new ChatDAO();

        public void AddMessage(Models.Message message)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Messages VALUES(@depDate, @fromUser, @toUser, @idChat, @content);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add(new SqlParameter("@depDate", message.DepartureDate));
                    command.Parameters.Add(new SqlParameter("@fromUser", message.FromUser.Login));
                    command.Parameters.Add(new SqlParameter("@toUser", message.ToUser.Login));
                    command.Parameters.Add(new SqlParameter("@idChat", message._Chat.Id));
                    command.Parameters.Add(new SqlParameter("@content", message.Content));
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
        public List<Models.Message> GetMessagesByChat(int chatId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Messages WHERE IdChat=@idChat;", connection);
                    command.Parameters.Add(new SqlParameter("@idChat", chatId));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Models.Message> messages = new List<Models.Message>();
                            while (reader.Read())
                            {
                                User fromUser = _userDAO.GetUserByLogin(reader.GetString(2));
                                User toUser = _userDAO.GetUserByLogin(reader.GetString(3));
                                string content = reader.GetString(5);
                                Chat chat = _chatDAO.GetChatById(reader.GetInt32(4));
                                Models.Message message = new Models.Message(fromUser, toUser, chat, content);
                                message.Id = reader.GetInt32(0);
                                message.DepartureDate = reader.GetDateTime(1);
                                messages.Add(message);
                            }
                            return messages;
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

        public async Task<List<Models.Message>> GetMessagesByChatAsync(int chatId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand("SELECT * FROM Messages WHERE IdChat=@idChat;", connection);
                    command.Parameters.Add(new SqlParameter("@idChat", chatId));
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            List<Models.Message> messages = new List<Models.Message>();
                            while (await reader.ReadAsync())
                            {
                                User fromUser = await _userDAO.GetUserByLoginAsync(reader.GetString(2));
                                User toUser = await _userDAO.GetUserByLoginAsync(reader.GetString(3));
                                string content = reader.GetString(5);
                                Chat chat = await _chatDAO.GetChatByIdAsync(reader.GetInt32(4));
                                Models.Message message = new Models.Message(fromUser, toUser, chat, content);
                                message.Id = reader.GetInt32(0);
                                message.DepartureDate = reader.GetDateTime(1);
                                messages.Add(message);
                            }
                            return messages;
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