using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinfromsChat.DAO;
using WinfromsChat.Models;

namespace WinfromsChat
{
    public partial class AddChatForm : Form
    {
        User _admin;
        public Chat NewChat { get; set; }
        public AddChatForm(User admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = tbUserLogin.Text;
            string chatName = tbChatName.Text;
            ChatDAO chatDAO = new ChatDAO();
            UserDAO userDAO = new UserDAO();
            Chat chat = new Chat(_admin, userDAO.GetUserByLogin(login), chatName);
            chatDAO.AddChat(chat);
            NewChat = chat;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
