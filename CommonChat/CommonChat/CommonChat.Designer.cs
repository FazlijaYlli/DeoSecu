namespace CommonChat
{
    partial class CommonChat
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonChat));
            this.sendBtn = new System.Windows.Forms.Button();
            this.msgBox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.addFriendBtn = new System.Windows.Forms.Button();
            this.deleteFriendBtn = new System.Windows.Forms.Button();
            this.infoBtn = new System.Windows.Forms.Button();
            this.renameFriendBtn = new System.Windows.Forms.Button();
            this.sendKeyBtn = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Location = new System.Drawing.Point(533, 319);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(90, 47);
            this.sendBtn.TabIndex = 5;
            this.sendBtn.Text = "Envoyer";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // msgBox
            // 
            this.msgBox.Enabled = false;
            this.msgBox.Location = new System.Drawing.Point(12, 319);
            this.msgBox.Multiline = true;
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(514, 47);
            this.msgBox.TabIndex = 3;
            this.msgBox.TextChanged += new System.EventHandler(this.msgBox_TextChanged);
            this.msgBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msgBox_KeyDown);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 90);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(614, 219);
            this.tabControl.TabIndex = 14;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(606, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(606, 193);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // addFriendBtn
            // 
            this.addFriendBtn.Location = new System.Drawing.Point(14, 13);
            this.addFriendBtn.Name = "addFriendBtn";
            this.addFriendBtn.Size = new System.Drawing.Size(200, 34);
            this.addFriendBtn.TabIndex = 15;
            this.addFriendBtn.Text = "Ajouter un contact";
            this.addFriendBtn.UseVisualStyleBackColor = true;
            this.addFriendBtn.Click += new System.EventHandler(this.addFriendBtn_Click);
            // 
            // deleteFriendBtn
            // 
            this.deleteFriendBtn.Location = new System.Drawing.Point(426, 13);
            this.deleteFriendBtn.Name = "deleteFriendBtn";
            this.deleteFriendBtn.Size = new System.Drawing.Size(200, 34);
            this.deleteFriendBtn.TabIndex = 16;
            this.deleteFriendBtn.Text = "Supprimer le contact";
            this.deleteFriendBtn.UseVisualStyleBackColor = true;
            this.deleteFriendBtn.Click += new System.EventHandler(this.deleteFriendBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Location = new System.Drawing.Point(14, 53);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(612, 24);
            this.infoBtn.TabIndex = 17;
            this.infoBtn.Text = "Mes infos";
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // renameFriendBtn
            // 
            this.renameFriendBtn.Location = new System.Drawing.Point(220, 13);
            this.renameFriendBtn.Name = "renameFriendBtn";
            this.renameFriendBtn.Size = new System.Drawing.Size(200, 34);
            this.renameFriendBtn.TabIndex = 18;
            this.renameFriendBtn.Text = "Renommer le contact";
            this.renameFriendBtn.UseVisualStyleBackColor = true;
            this.renameFriendBtn.Click += new System.EventHandler(this.renameFriendBtn_Click);
            // 
            // sendKeyBtn
            // 
            this.sendKeyBtn.Location = new System.Drawing.Point(12, 372);
            this.sendKeyBtn.Name = "sendKeyBtn";
            this.sendKeyBtn.Size = new System.Drawing.Size(612, 24);
            this.sendKeyBtn.TabIndex = 19;
            this.sendKeyBtn.Text = "Envoyer ma clé publique";
            this.sendKeyBtn.UseVisualStyleBackColor = true;
            this.sendKeyBtn.Click += new System.EventHandler(this.sendKeyBtn_Click);
            // 
            // CommonChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 403);
            this.Controls.Add(this.sendKeyBtn);
            this.Controls.Add(this.renameFriendBtn);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.deleteFriendBtn);
            this.Controls.Add(this.addFriendBtn);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.sendBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CommonChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Common Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommonChat_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button addFriendBtn;
        private System.Windows.Forms.Button deleteFriendBtn;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button infoBtn;
        private System.Windows.Forms.Button renameFriendBtn;
        private System.Windows.Forms.Button sendKeyBtn;
    }
}

