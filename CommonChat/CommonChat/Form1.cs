/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe Form1
 */

using System;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;

namespace CommonChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string messageToSend = msgBox.Text;
            msgBox.Text = String.Empty;

            // Tableau de bytes
            byte[] msg = Encoding.Default.GetBytes(messageToSend);

            UdpClient udpClient = new UdpClient();

            string ipDestination = "127.0.0.1";
            int portDestination = 5053;

            udpClient.Connect(ipDestination, portDestination);
            udpClient.Send(msg, msg.Length);

            udpClient.Close();
            
            listBox.Items.Add(ipDestination + ":" + portDestination + " > " + messageToSend);
        }
    }
}
