﻿namespace CommonChat
{
    partial class AddFriend
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
            this.labelName = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(15, 13);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(37, 16);
            this.labelName.TabIndex = 22;
            this.labelName.Text = "Nom";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(18, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(157, 20);
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_KeyDown);
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(18, 136);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(157, 20);
            this.portTextBox.TabIndex = 2;
            this.portTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_KeyDown);
            // 
            // changeButton
            // 
            this.changeButton.Enabled = false;
            this.changeButton.Location = new System.Drawing.Point(192, 111);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(0, 0);
            this.changeButton.TabIndex = 20;
            this.changeButton.Text = "Changer";
            this.changeButton.UseVisualStyleBackColor = true;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIP.Location = new System.Drawing.Point(15, 65);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(20, 16);
            this.labelIP.TabIndex = 17;
            this.labelIP.Text = "IP";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort.Location = new System.Drawing.Point(15, 117);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(32, 16);
            this.labelPort.TabIndex = 18;
            this.labelPort.Text = "Port";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(18, 84);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(157, 20);
            this.ipTextBox.TabIndex = 1;
            this.ipTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Enter_KeyDown);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(18, 178);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(157, 29);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Ajouter";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // AddFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 225);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.ipTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFriend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajout d\'un contact";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button runButton;
    }
}