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
            if(!TestServerAccesibility(hostText.Text))
            {
                // not valid
            }
            else
            {
                if(Convert.ToInt32(portText.Text) > 5000)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    Close();
                }
            }
        }

        /// <summary>
        /// Le serveur est-il accessible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool TestServerAccesibility(string IP)
        {
            try
            {
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(IP));
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("L'adresse IP rentrée est invalide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
