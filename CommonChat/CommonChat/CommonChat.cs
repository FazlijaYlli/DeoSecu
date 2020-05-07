/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe CommonChat
 */

using System;
using System.Collections.Generic;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CommonChat
{
    public partial class CommonChat : Form
    {
        public static TabControl TabControlStatic;
        public static TextBox MsgBoxStatic;
        public static Button SendBtnStatic;
        public static int LocalPort;
        public static string LocalPublicKey;
        public static UdpClient Client;
        public static Dictionary<string, ListBox> FriendsChat;

        private static Thread ListeningThread;

        private bool _threadLoop;
        private SoundPlayer _newMessage;
        private string _localPrivateKey;

        public CommonChat()
        {
            InitializeComponent();

            FriendsChat = new Dictionary<string, ListBox>();
            Client = new UdpClient();
            _newMessage = new SoundPlayer(@"..\..\Resources\newMessage.wav");

            TabControlStatic = tabControl;
            MsgBoxStatic = msgBox;
            SendBtnStatic = sendBtn;

            tabControl.TabPages.Clear();

            RSATools.GenerateKeys(out LocalPublicKey, out _localPrivateKey);
            Database.GetFriends();

            LocalPort = Database.GetLocalPort();

            // Si aucun ami n'a été ajouté
            if (TabControlStatic.TabCount == 0)
            {
                sendBtn.Enabled = false;
                msgBox.Enabled = false;
            }

            _threadLoop = true;
            Restart_Thread();
        }


        /// <summary>
        /// Bouton envoyer le message au serveur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendBtn_Click(object sender, EventArgs e)
        {
            Database.SendMessage(msgBox.Text, false, false);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count != 0)
            {
                Database.ConnectToFriend(tabControl.SelectedTab.Text);
                MsgBoxStatic.Focus();
            }
        }

        /// <summary>
        /// Open the addFriend Form to add a new Friend.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFriendBtn_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "AddFriend")
                {
                    isOpen = true;
                    f.BringToFront();
                }
            }

            if (!isOpen)
            {
                AddFriend addFriendForm = new AddFriend();
                addFriendForm.Show();
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, active sendBtn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // n'écrit pas le retour à la ligne
                e.SuppressKeyPress = true;
                if (sendBtn.Enabled)
                {
                    sendBtn_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Remove the current friend / tabpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteFriendBtn_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count != 0)
            {
                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer " + tabControl.SelectedTab.Text + " de vos amis ?\nCette action sera irréversible.", "Suppression d'ami", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Database.RemoveFriend(tabControl.SelectedTab.Text);

                    if (tabControl.TabPages.Count == 0)
                    {
                        sendBtn.Enabled = false;
                        msgBox.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Vous n'avez pas d'onglet de contact ouvert.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Fait apparaître une fenêtre d'informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoBtn_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "MyInfo")
                {
                    isOpen = true;
                    f.BringToFront();
                }
            }

            if (!isOpen)
            {
                MyInfo myInfoForm = new MyInfo();
                myInfoForm.Show();
            }
        }

        /// <summary>
        /// Active le bouton envoyé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msgBox_TextChanged(object sender, EventArgs e)
        {
            if (msgBox.Text.Length > 0 && msgBox.Text.Length < 255)
            {
                sendBtn.Enabled = true;
            }
            else
            {
                sendBtn.Enabled = false;
            }
        }

        /// <summary>
        /// Reset and restart a new listening thread
        /// </summary>
        private void Restart_Thread()
        {
            ListeningThread = new Thread(new ThreadStart(Listen));
            ListeningThread.Start();
        }

        /// <summary>
        /// Ecoute en permanance le trafic sur le port d'écoute
        /// </summary>
        private void Listen()
        {
            UdpClient server = null;

            try
            {
                server = new UdpClient(LocalPort);
            }
            catch
            {
                // impossible d'accéder au port
                Thread.Sleep(100);
                return;
            }

            server.Client.ReceiveTimeout = 1000;

            while (_threadLoop)
            {
                try
                {
                    // verifie pour chaque adresse enregistrée le trafic entrant
                    foreach (string ip in Database.GetFriendsIP())
                    {
                        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), LocalPort);
                        byte[] data = server.Receive(ref ep);

                        string uncryptedMsg = Encoding.UTF8.GetString(data);

                        // Si le message en dur (avant décryptage) et qu'il provient du bon EndPoint.
                        if (uncryptedMsg == "RESET_KEY")
                        {
                            Invoke(new Action<string, string>(Database.SetKey), Database.GetNameByIP(ep.Address), "WAITING_FOR_KEY");
                        }
                        
                        const string BEGINNING = "<RSAKeyValue><Modulus>";
                        const string MIDDLE = "</Modulus><Exponent>";
                        const string ENDING = "</Exponent></RSAKeyValue>";

                        // Vérifie que c'est une clé publique valide
                        if (data.Length == 415)
                        {
                            if (uncryptedMsg.Substring(0, BEGINNING.Length) == BEGINNING && uncryptedMsg.Substring(uncryptedMsg.Length - ENDING.Length - 4 - MIDDLE.Length, MIDDLE.Length) == MIDDLE && uncryptedMsg.Substring(uncryptedMsg.Length - ENDING.Length, ENDING.Length) == ENDING)
                            {
                                // Remplace la clé de l'ami actuel
                                Invoke(new Action<string, string>(Database.SetKey), Database.GetNameByIP(ep.Address), uncryptedMsg);
                                msgBox.Enabled = true;
                                continue;
                            }
                        }

                        string decryptedMsg = Encoding.UTF8.GetString(RSATools.RSADecrypt(data, _localPrivateKey, false));

                        Invoke(new Action<string, string>(AddLog), decryptedMsg, Database.GetNameByIP(ep.Address));
                    }
                }
                catch
                {
                    // do nothing
                }
            }

            server.Close();
        }

        /// <summary>
        /// Ajoute le message reçu à la ListBox
        /// </summary>
        /// <param name="msg"></param>
        private void AddLog(string msg, string remoteName)
        {
            if (Database.HasKey(tabControl.SelectedTab.Text))
            {
                FriendsChat[remoteName].Items.Add(remoteName + " [" + DateTime.Now + "] > " + msg);
                if(WindowState == FormWindowState.Minimized)
                {
                    FlashWin.Flash(this);
                    _newMessage.Play();
                }
            }
        }

        /// <summary>
        /// Fait apparaître la fenêtre de modification de contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameFriendBtn_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count != 0)
            {
                bool isOpen = false;

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "ModifyFriend")
                    {
                        isOpen = true;
                        f.BringToFront();
                    }
                }

                if (!isOpen)
                {
                    RenameFriend modifyFriendForm = new RenameFriend();
                    modifyFriendForm.Show();
                }
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Vous n'avez pas d'onglet de contact ouvert.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Arrête la boucle d'écoute et le thread avant de quitter l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tabControl.TabPages.Count != 0)
            {
                // Envoie un message à tous les contacts pour reset ma clé publique de cette session
                foreach(TabPage tabPage in tabControl.TabPages)
                {
                    tabControl.SelectedTab = tabPage;
                    Database.SendMessage("RESET_KEY", true, true);
                    // Reset total des clés de tous les contacts à la fermture.
                    Database.SetKey(tabPage.Text, "WAITING_FOR_KEY");
                }
            }

            _threadLoop = false;
            ListeningThread.Join();
            Client.Close();
        }

        /// <summary>
        /// Envoie notre clé publique à l'ami actif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendKeyBtn_Click(object sender, EventArgs e)
        {
            Database.SendMessage(LocalPublicKey, true, false);
            MessageBox.Show(new Form() { TopMost = true }, "Votre clé publique a bien été envoyée.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}