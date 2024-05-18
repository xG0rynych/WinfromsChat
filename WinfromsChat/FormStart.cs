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
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            UserDAO uDao = new UserDAO();
            User user = uDao.UserAuthorization(tbUserLogin.Text, tbUserPassword.Text);
            if(user!=null)
            {
                MainForm mainForm = new MainForm(user);
                mainForm.Show();
                mainForm.Owner = this;
                Hide();
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            UserDAO uDao = new UserDAO();
            User user = new User(tbUserLogin.Text, tbUserPassword.Text);
            if(uDao.AddUser(user))
            {
                MainForm mainForm = new MainForm(user);
                mainForm.Show();
                mainForm.Owner = this;
                Hide();
            }
        }
    }
}
