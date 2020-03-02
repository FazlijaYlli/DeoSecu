/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe Form1
 */

using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CommonChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton envoyer le message au serveur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendBtn_Click(object sender, EventArgs e)
        {

            string messageToSend = msgBox.Text;
            msgBox.Text = "";

            // Tableau de bytes
            byte[] msg = Encoding.Default.GetBytes(messageToSend);

            UdpClient udpClient = new UdpClient();

            string ipDestination = "10.228.156.180";
            int portDestination = 5053;

            udpClient.Connect(ipDestination, portDestination);
            udpClient.Send(msg, msg.Length);

            udpClient.Close();
            
            listBox.Items.Add(ipDestination + ":" + portDestination + " > " + messageToSend);

            // resélectionne la textbox pour écrire
            msgBox.Select();
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, envoie le message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // n'écrit pas le retour à la ligne
                e.SuppressKeyPress = true;
                sendBtn_Click(sender, e);
            }
        }
    }
}
