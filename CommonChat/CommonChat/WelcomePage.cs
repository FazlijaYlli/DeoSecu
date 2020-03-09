/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe welcomePage
 */

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace CommonChat
{
    public partial class welcomePage : Form
    {
        public welcomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(TestServerAccesibility(serverText.Text))
            {
                Form1 form1 = new Form1(serverText.Text, Convert.ToInt32(portText.Text));
                form1.Show();
                Close();
            }
        }

        /// <summary>
        /// Test de l'accessibilité du serveur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool TestServerAccesibility(string IP)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(IPAddress.Parse(IP));

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("L'adresse IP rentrée n'est pas atteignable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("L'adresse IP rentrée est invalide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, démarre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // n'écrit pas le retour à la ligne
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }

        /// <summary>
        /// Quitte le programme en entier en cas de fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void welcomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void portText_TextChanged(object sender, EventArgs e)
        {
            if(portText.Text != "" && serverText.Text != "")
            {
                button1.Enabled = true;
            }
        }
    }
}
