/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe Form1
 */

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

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

            string ipDestination = serverText.Text;
            int portDestination = Convert.ToInt32(portText.Text);

            udpClient.Connect(ipDestination, portDestination);
            udpClient.Send(msg, msg.Length);

            udpClient.Close();
            
            listBox.Items.Add(ipDestination + ":" + portDestination + " > " + messageToSend);

            // resélectionne la textbox pour écrire
            msgBox.Select();
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
        /// Test la validité du port
        /// </summary>
        private bool TestPort(string port)
        {
            int newPort;
            // est-ce un nombre ?
            bool valid = int.TryParse(port, out newPort);

            if (valid)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Veuillez rentrer un port valide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (TestServerAccesibility(serverText.Text) && TestPort(portText.Text))
            {
                portText.Enabled = false;
                serverText.Enabled = false;
                runButton.Enabled = false;
                changeButton.Enabled = true;
                sendBtn.Enabled = true;
                msgBox.Enabled = true;

                msgBox.Select();

                connectionLabel.ForeColor = Color.Green;
                connectionLabel.Text = "Online";

                GenerateKeys();
                listBox.Items.Add("Échange des clés publiques en cours ...");
                
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, démarre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portText_TextChanged(object sender, EventArgs e)
        {
            if (portText.Text != "" && serverText.Text != "")
            {
                runButton.Enabled = true;
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, démarre la connexion ou envoie le message
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
        /// Lorsque l'utilisateur veut changer de serveur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeButton_Click(object sender, EventArgs e)
        {
            connectionLabel.ForeColor = Color.Red;

            connectionLabel.Text = "Offline";
            serverText.Text = "";
            portText.Text = "";

            changeButton.Enabled = false;
            serverText.Enabled = true;
            portText.Enabled = true;
            sendBtn.Enabled = false;
            msgBox.Enabled = false;

            listBox.Items.Clear();

            serverText.Focus();
        }

        /// <summary>
        /// Crée une clé publique et privée pour la session
        /// </summary>
        private RSAParameters[] GenerateKeys()
        {
            // Création du provider de clés sur 2048 bits
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            RSAParameters publicKey = rsa.ExportParameters(false);
            RSAParameters privateKey = rsa.ExportParameters(true);

            // Stocke les clés dans un tableau
            RSAParameters[] keys = { publicKey, privateKey };

            listBox.Items.Add("Création des clés effectuée !");

            return keys;
        }

        /// <summary>
        /// Encrypte le message en RSA
        /// </summary>
        private byte[] Encrypt(byte[] input)
        {

        }
    }
}