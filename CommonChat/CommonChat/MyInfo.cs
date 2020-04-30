/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe MyInfo
 */

using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;

namespace CommonChat
{
    public partial class MyInfo : Form
    {
        public MyInfo()
        {
            InitializeComponent();
            GetLocalIPAddress();
            labelMyPort.Text = Convert.ToString(CommonChat.LocalPort);
        }

        /// <summary>
        /// Fonction récupérée de https://stackoverflow.com/questions/5271724/get-all-ip-addresses-on-machine
        /// </summary>
        /// <returns></returns>
        public void GetLocalIPAddress()
        {
            int y = 40;
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var row = new string[] { netInterface.Name, ip.Address.ToString() };
                            listViewIP.Items.Add(new ListViewItem(row));
                            //Créations des boutons pour copier dans le presse-papier
                            groupBox1.Controls.Add(new Button
                            {
                                Location = new Point(385, y),
                                Size = new Size(60, 20),
                                Text = "Copier",
                                Font = new Font(Font.FontFamily, 8),
                            });
                            //Controls[Controls.Count - 1].BringToFront();
                            groupBox1.Controls[groupBox1.Controls.Count - 1].Click += new EventHandler(CopyButton);
                            y += 19;
                        }
                    }
                }
            }
        }

        private void CopyButton(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = (btn.Location.Y - 40) / 19;
            string data = listViewIP.Items[index].SubItems[1].Text;
            Clipboard.SetData(DataFormats.Text, data);
            MessageBox.Show(new Form() { TopMost = true }, "L'adresse IP a bien été copiée dans le presse-papier.", "Succès !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Modifie le port d'écoute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifyBtn_Click(object sender, EventArgs e)
        {
            // minimum un nombre à 4 chiffres
            if (newPortTextBox.Text.Length >= 4)
            {
                if (Int32.TryParse(newPortTextBox.Text, out int newPort))
                {
                    DialogResult result = MessageBox.Show(new Form() { TopMost = true }, "Cela implique un redémarrage, voulez-vous redémarrer maintenant ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Reset de ma clé chez tout le monde => communication désormais impossible sur ce port.
                        Database.SendMessage("RESET_KEY", true, true);
                        Database.SetLocalPort(newPort.ToString());
                        labelMyPort.Text = newPort.ToString();
                        Application.Restart();
                    }
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "Le port doit être un nombre", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Le port doit être un nombre à 4 chiffres ou plus", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            newPortTextBox.Text = "";
            modifyBtn.Enabled = false;
        }

        private void newPortTextBox_TextChanged(object sender, EventArgs e)
        {
            modifyBtn.Enabled = true;

        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, envoie le message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // n'écrit pas le retour à la ligne
                e.SuppressKeyPress = true;
                modifyBtn_Click(sender, e);
            }
        }
    }
}