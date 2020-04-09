namespace CommonChat
{
    partial class MyInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewIP = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newPortTextBox = new System.Windows.Forms.TextBox();
            this.modifyBtn = new System.Windows.Forms.Button();
            this.labelMyPort = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewIP);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 216);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vos IP";
            // 
            // listViewIP
            // 
            this.listViewIP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewIP.HideSelection = false;
            this.listViewIP.Location = new System.Drawing.Point(10, 19);
            this.listViewIP.Name = "listViewIP";
            this.listViewIP.Size = new System.Drawing.Size(364, 190);
            this.listViewIP.TabIndex = 0;
            this.listViewIP.UseCompatibleStateImageBehavior = false;
            this.listViewIP.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nom interface réseau";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Adresse IP";
            this.columnHeader2.Width = 160;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newPortTextBox);
            this.groupBox2.Controls.Add(this.modifyBtn);
            this.groupBox2.Controls.Add(this.labelMyPort);
            this.groupBox2.Location = new System.Drawing.Point(15, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 55);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port d\'écoute de la session";
            // 
            // newPortTextBox
            // 
            this.newPortTextBox.Location = new System.Drawing.Point(172, 22);
            this.newPortTextBox.Name = "newPortTextBox";
            this.newPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.newPortTextBox.TabIndex = 28;
            this.newPortTextBox.TextChanged += new System.EventHandler(this.newPortTextBox_TextChanged);
            this.newPortTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_KeyDown);
            // 
            // modifyBtn
            // 
            this.modifyBtn.Enabled = false;
            this.modifyBtn.Location = new System.Drawing.Point(278, 20);
            this.modifyBtn.Name = "modifyBtn";
            this.modifyBtn.Size = new System.Drawing.Size(96, 23);
            this.modifyBtn.TabIndex = 27;
            this.modifyBtn.Text = "Modifier";
            this.modifyBtn.UseVisualStyleBackColor = true;
            this.modifyBtn.Click += new System.EventHandler(this.modifyBtn_Click);
            // 
            // labelMyPort
            // 
            this.labelMyPort.AutoSize = true;
            this.labelMyPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMyPort.ForeColor = System.Drawing.Color.Green;
            this.labelMyPort.Location = new System.Drawing.Point(11, 23);
            this.labelMyPort.Name = "labelMyPort";
            this.labelMyPort.Size = new System.Drawing.Size(36, 16);
            this.labelMyPort.TabIndex = 26;
            this.labelMyPort.Text = "Port";
            // 
            // MyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 301);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mes informations";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelMyPort;
        private System.Windows.Forms.ListView listViewIP;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button modifyBtn;
        private System.Windows.Forms.TextBox newPortTextBox;
    }
}