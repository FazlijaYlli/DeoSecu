using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CommonChatServer
{
    public partial class ServerForm : Form
    {
        private bool loop;
        private Thread thrdListen;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            consoleListBox.Items.Add("Préparation à l'écoute...");

            //Initialisation des objets nécessaires au client. On lance également le thread qui en charge d'écouter.
            loop = true;
            thrdListen = new Thread(new ThreadStart(ThreadListen));
            thrdListen.Start();
        }

        private void ThreadListen()
        {
            //Déclaration du Socket d'écoute.
            UdpClient server = null;

            //Création sécurisée du Socket.
            try
            {
                server = new UdpClient(5053);
            }
            catch
            {
                MessageBox.Show("Impossible de se lier au port UDP 5053. Vérifiez vos configurations réseau.");
                return;
            }

            //Définition du Timeout.
            server.Client.ReceiveTimeout = 1000;

            //Bouclage infini d'écoute de port.
            while (loop)
            {
                try
                {
                    IPEndPoint ip = null;
                    byte[] data = server.Receive(ref ip);
                    string adress = ip.Address.ToString();
                    string port = ip.Port.ToString();

                    //Invocation du la méthode AjouterLog afin que les données soient inscrites dans
                    //la TextBox.
                    Invoke(new Action<string>(AddLog), " : " + adress + ":" + port + " [" + Encoding.Default.GetString(data) + "]");
                }
                catch
                {

                }
            }

            server.Close();
        }

        private void AddLog(string data)
        {
            consoleListBox.Items.Add(DateTime.Now + "\r\n" + " => " + data);
        }
    }
}
