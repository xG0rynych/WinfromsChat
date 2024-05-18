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
    public partial class MainForm : Form
    {
        private UserDAO _uDao;
        private ChatDAO _cDao;
        private MessageDAO _mDao;
        private User _user;
        private Chat _currentChat = null;

        private Timer _timerChats;
        private Timer _timerMessage;

        public MainForm(User user)
        {
            InitializeComponent();
            _user = user;
            _uDao = new UserDAO();
            _cDao = new ChatDAO();
            _mDao = new MessageDAO();

            DisplayUserData();
            InitializeTimerChat();
            InitializeTimerMessage();
        }

        //
        private void InitializeTimerChat()
        {
            _timerChats = new Timer();
            _timerChats.Interval = 10000; // 10 секунд
            _timerChats.Tick += async (s, e) => await GetChatsEvent();
            _timerChats.Start();
        }

        private void InitializeTimerMessage()
        {
            _timerMessage = new Timer();
            _timerChats.Interval = 5000;
            _timerChats.Tick += async (s, e) => await GetMessagesEvent();
            _timerChats.Start();
        }

        private async Task GetMessagesEvent()
        {
            if(_currentChat!=null)
            {
                List<Models.Message> messages = await _mDao.GetMessagesByChatAsync(_currentChat.Id);
                if(messages != null) // Здесь ошибка: NullReferenceException
                {
                    _currentChat.Messages = messages;
                    ShowMessagesTimer();
                }
            }
        }

        private async Task GetChatsEvent()
        {
            List<Chat> chats = await _cDao.GetChatsByLoginAsync(_user.Login);
            if(chats!=null && chats.Count!=_user.Chats.Count)
            {
                _user.Chats = chats;
                DisplayUserDataTimer();
            }
        }

        private void DisplayUserDataTimer()
        {
            pnChats.Controls.Clear();
            if (_user.Chats != null)
            {
                for (int i = 0; i < _user.Chats.Count; i++)
                {
                    Button chatButton = new Button()
                    {
                        Width = lbLogin.Width - 20,
                        Height = lbLogin.Height,
                        Name = $"btnChat{_user.Chats[i].Id}",
                        Font = lbLogin.Font,
                        Text = $"{_user.Chats[i].ChatName}"
                    };
                    chatButton.Click += ShowMessages;
                    chatButton.Location = new Point(0, i * chatButton.Height + i * 10 + chatButton.Height - 60);
                    pnChats.Controls.Add(chatButton);
                }
            }
        }

        private void ShowMessagesTimer()
        {
            pnMessages.Controls.Clear();
            rtbMessageContent.Visible = true;
            btnSendMessage.Visible = true;
            if (_currentChat.Messages != null)
            {
                for (int i = 0; i < _currentChat.Messages.Count; i++)
                {
                    Label message = new Label()
                    {
                        Width = lbLogin.Width,
                        Text = $"{_currentChat.Messages[i].FromUser.Login}\n{_currentChat.Messages[i].Content}\n{_currentChat.Messages[i].DepartureDate}",
                        AutoSize = false,
                        Font = new Font("Arial", 12),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    message.Height = GetMessageHeight(message);
                    if (_currentChat.Messages[i].FromUser.Login.Equals(_user.Login))
                    {
                        message.Location = new Point(120 + lbLogin.Width, i * message.Height + i * 10 + message.Height);
                    }
                    else
                    {
                        message.Location = new Point(20, i * message.Height + i * 10 + message.Height);
                    }
                    pnMessages.Controls.Add(message);
                }
            }
        }
        //

        private void DisplayUserData()
        {
            pnChats.Controls.Clear();
            _user.Chats = _cDao.GetChatsByLogin(_user.Login);
            lbLogin.Text = _user.Login;

            if (_user.Chats != null)
            {
                for (int i = 0; i < _user.Chats.Count; i++)
                {
                    Button chatButton = new Button()
                    {
                        Width = lbLogin.Width-20,
                        Height = lbLogin.Height,
                        Name = $"btnChat{_user.Chats[i].Id}",
                        Font = lbLogin.Font,
                        Text = $"{_user.Chats[i].ChatName}"
                    };
                    chatButton.Click += ShowMessages;
                    chatButton.Location = new Point(0, i*chatButton.Height+i*10+chatButton.Height-60);
                    pnChats.Controls.Add(chatButton);
                }
            }
        }

        private int FirstDigitIndex(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private void ShowMessages(object sender, EventArgs e)
        {
            pnMessages.Controls.Clear();
            Button btn = (Button)sender;

            int id = int.Parse(btn.Name.Substring(FirstDigitIndex(btn.Name)));

            Chat chat = _user.GetChatById(id);
            _currentChat = chat;
            chat.Messages = _mDao.GetMessagesByChat(chat.Id);
            rtbMessageContent.Visible = true;
            btnSendMessage.Visible = true;
            if(chat.Messages!=null)
            {
                for (int i = 0; i < chat.Messages.Count; i++)
                {
                    Label message = new Label()
                    {
                        Width = lbLogin.Width,
                        Text = $"{chat.Messages[i].FromUser.Login}\n{chat.Messages[i].Content}\n{chat.Messages[i].DepartureDate}",
                        AutoSize = false,
                        Font = new Font("Arial", 12),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    message.Height = GetMessageHeight(message);
                    if (chat.Messages[i].FromUser.Login.Equals(_user.Login))
                    {
                        message.Location = new Point(120+lbLogin.Width, i * message.Height + i*10 + message.Height);
                    }
                    else
                    {
                        message.Location = new Point(20, i * message.Height + i * 10 + message.Height);
                    }
                    pnMessages.Controls.Add(message);
                }
            }
        }

        private int GetMessageHeight(Label label)
        {
            using (Graphics g = label.CreateGraphics())
            {
                SizeF size = g.MeasureString(label.Text, label.Font, label.Width);
                return (int)Math.Ceiling(size.Height);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Dispose();
            _timerChats.Dispose();
            _timerMessage.Dispose();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string content = rtbMessageContent.Text;
            if(_currentChat.UserAdmin.Login.Equals(_user.Login))
            {
                Models.Message message = new Models.Message(_user, _currentChat._User, _currentChat, content);
                _mDao.AddMessage(message);
                if(_currentChat.Messages==null)
                {
                    _currentChat.Messages = new List<Models.Message>();
                }
                _currentChat.AddMessage(message);
                Label lbMessage = new Label()
                {
                    Width = lbLogin.Width,
                    Text = $"{message.FromUser.Login}\n{content}\n{message.DepartureDate}",
                    AutoSize = false,
                    Font = new Font("Arial", 12),
                    BorderStyle = BorderStyle.FixedSingle
                };
                lbMessage.Height = GetMessageHeight(lbMessage);
                if(_currentChat.Messages.Count==0)
                {
                    lbMessage.Location = new Point(120 + lbLogin.Width, 0 * lbMessage.Height + 0 * 10 + lbMessage.Height);
                }
                else
                {
                    lbMessage.Location = new Point(120 + lbLogin.Width, (_currentChat.Messages.Count - 1) * lbMessage.Height + (_currentChat.Messages.Count - 1) * 10 + lbMessage.Height);
                }
                pnMessages.Controls.Add(lbMessage);

            }
            else
            {
                Models.Message message = new Models.Message(_user, _currentChat.UserAdmin, _currentChat, content);
                _mDao.AddMessage(message);
                _currentChat.AddMessage(message);//
                Label lbMessage = new Label()
                {
                    Width = lbLogin.Width,
                    Text = $"{message.FromUser.Login}\n{content}\n{message.DepartureDate}",
                    AutoSize = false,
                    Font = new Font("Arial", 12),
                    BorderStyle = BorderStyle.FixedSingle
                };
                lbMessage.Height = GetMessageHeight(lbMessage);
                if(_currentChat.Messages.Count==0)
                {
                    lbMessage.Location = new Point(20, 0 * lbMessage.Height + 0 * 10 + lbMessage.Height);
                }
                else
                {
                    lbMessage.Location = new Point(20, (_currentChat.Messages.Count - 1) * lbMessage.Height + (_currentChat.Messages.Count - 1) * 10 + lbMessage.Height);
                }
                pnMessages.Controls.Add(lbMessage);
            }
        }

        private void btnAddChat_Click(object sender, EventArgs e)
        {
            AddChatForm addChatForm = new AddChatForm(_user);
            if(addChatForm.ShowDialog() == DialogResult.OK)
            {
                if(_user.Chats==null)
                {
                    _user.Chats = new List<Chat>
                    {
                        addChatForm.NewChat
                    };
                }
                else
                {
                    _user.Chats.Add(addChatForm.NewChat);
                }
            }
            DisplayUserData();
        }
    }
}
