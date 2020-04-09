/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe AddFriend
 */

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace CommonChat
{
    public partial class AddFriend : Form
    {
        public AddFriend()
        {
            InitializeComponent();
            // Port par défaut : 5050
            portTextBox.Text = Convert.ToString(5050);
        }

        /// <summary>
        /// Ajoute l'ami à la base de donnée et à la TabControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != String.Empty && ipTextBox.Text != String.Empty && portTextBox.Text != String.Empty)
            {
                if (TestPort(portTextBox.Text) && TestServerAccesibility(ipTextBox.Text))
                {
                    if (Database.IsNewFriendValid(nameTextBox.Text, ipTextBox.Text))
                    {
                        Database.AddFriendToDB(nameTextBox.Text, ipTextBox.Text, portTextBox.Text);
                    }
                    else
                    {
                        // sort de la fonction
                        return;
                    }

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Veuillez remplir tous les champs !", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Test la validité du port
        /// </summary>
        private bool TestPort(string port)
        {
            if (Int32.TryParse(port, out _))
            {
                return true;
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Veuillez rentrer un port valide", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                if (IP == "127.0.0.1")
                {
                    throw new FormatException("L'adresse IP ne peut pas être 127.0.0.1");
                }

                Ping ping = new Ping();
                PingReply reply = ping.Send(IPAddress.Parse(IP));

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "L'adresse IP rentrée n'est pas atteignable", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(new Form() { TopMost = true }, "L'adresse IP rentrée est invalide", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, appuie sur le bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // n'écrit pas le retour à la ligne
                e.SuppressKeyPress = true;
                runButton_Click(sender, e);
            }
        }
    }
}