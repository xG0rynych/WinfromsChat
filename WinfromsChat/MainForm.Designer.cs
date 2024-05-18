namespace WinfromsChat
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnChats = new System.Windows.Forms.Panel();
            this.lbLogin = new System.Windows.Forms.Label();
            this.pnMessages = new System.Windows.Forms.Panel();
            this.rtbMessageContent = new System.Windows.Forms.RichTextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnAddChat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnChats
            // 
            this.pnChats.AutoScroll = true;
            this.pnChats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnChats.Location = new System.Drawing.Point(1, 70);
            this.pnChats.Name = "pnChats";
            this.pnChats.Size = new System.Drawing.Size(258, 403);
            this.pnChats.TabIndex = 0;
            // 
            // lbLogin
            // 
            this.lbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.Location = new System.Drawing.Point(5, 2);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(247, 65);
            this.lbLogin.TabIndex = 0;
            this.lbLogin.Text = "IvanFrolov";
            this.lbLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnMessages
            // 
            this.pnMessages.AutoScroll = true;
            this.pnMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMessages.Location = new System.Drawing.Point(258, 2);
            this.pnMessages.Name = "pnMessages";
            this.pnMessages.Size = new System.Drawing.Size(656, 334);
            this.pnMessages.TabIndex = 1;
            // 
            // rtbMessageContent
            // 
            this.rtbMessageContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessageContent.Location = new System.Drawing.Point(258, 342);
            this.rtbMessageContent.Name = "rtbMessageContent";
            this.rtbMessageContent.Size = new System.Drawing.Size(656, 131);
            this.rtbMessageContent.TabIndex = 2;
            this.rtbMessageContent.Text = "";
            this.rtbMessageContent.Visible = false;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.Location = new System.Drawing.Point(535, 479);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(130, 40);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Visible = false;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnAddChat
            // 
            this.btnAddChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddChat.Location = new System.Drawing.Point(66, 479);
            this.btnAddChat.Name = "btnAddChat";
            this.btnAddChat.Size = new System.Drawing.Size(105, 40);
            this.btnAddChat.TabIndex = 4;
            this.btnAddChat.Text = "Add chat";
            this.btnAddChat.UseVisualStyleBackColor = true;
            this.btnAddChat.Click += new System.EventHandler(this.btnAddChat_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 540);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.btnAddChat);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.rtbMessageContent);
            this.Controls.Add(this.pnMessages);
            this.Controls.Add(this.pnChats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnChats;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Panel pnMessages;
        private System.Windows.Forms.RichTextBox rtbMessageContent;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnAddChat;
    }
}