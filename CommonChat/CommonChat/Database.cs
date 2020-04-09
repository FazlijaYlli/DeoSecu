/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe Database
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CommonChat
{
    /// <summary>
    /// Classe intéragissant avec la DB de CommonChat
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Cherche le port d'écoute dans la BD sinon retourne 5053
        /// </summary>
        /// <returns>Port d'écoute (int)</returns>
        public static int GetLocalPort()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "myport")
                {
                    return Convert.ToInt32(node.InnerText);
                }
            }
            return 5053;
        }

        public static void SetLocalPort(string localPort)
        {
            // Add it to the XML file
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "myport")
                {
                    node.InnerText = localPort;
                }
            }
            doc.Save("users.xml");

            CommonChat.LocalPort = Convert.ToInt32(localPort);

            MessageBox.Show(new Form() { TopMost = true }, "Votre port d'écoute a bien été modifié", "Succès !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Renvoie les infos du contact de l'onglet actif
        /// </summary>
        public static string[] GetCurrentFriend()
        {
            string[] result = new string[3];
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend" && node.Attributes[0].InnerText == CommonChat.TabControlStatic.SelectedTab.Text)
                {
                    // nom
                    result[0] = node.Attributes[0].InnerText;
                    // ip
                    result[1] = node.ChildNodes[0].InnerText;
                    // port
                    result[2] = node.ChildNodes[1].InnerText;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Charge tous les amis de la DB sur le TabControl
        /// </summary>
        public static void GetFriends()
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists("users.xml"))
            {
                doc.Load("users.xml");

                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.Name == "friend")
                    {
                        string name = node.Attributes[0].InnerText;
                        // Create a ListView (chat) in a dictionnary which store the listview for each friend
                        CommonChat.FriendsChat.Add(name, new ListBox());
                        CommonChat.FriendsChat[name].Width = 606;
                        CommonChat.FriendsChat[name].Height = 193;
                        CommonChat.FriendsChat[name].HorizontalScrollbar = true;
                        // Add a tabpage for each friend of the database
                        CommonChat.TabControlStatic.TabPages.Add(name);
                        // Add the ListView in the TabPage created
                        CommonChat.TabControlStatic.TabPages[CommonChat.TabControlStatic.TabCount - 1].Controls.Add(CommonChat.FriendsChat[name]);

                        //Reset notre clé si problème chez notre cher ami
                        SetKey(name, "WAITING_FOR_KEY");

                        //Envoie ma nouvelle clé publique à chaque ami au début de la session
                        SendMessage(CommonChat.LocalPublicKey, true);

                        if (node.ChildNodes[2].InnerText == "WAITING_FOR_KEY")
                        {
                            CommonChat.FriendsChat[name].Items.Clear();
                            CommonChat.FriendsChat[name].Items.Add("Votre ami doit vous rajouter dans sa liste de contacts pour vous transmettre sa clé publique.");
                            CommonChat.FriendsChat[name].Items.Add("");
                            CommonChat.FriendsChat[name].Items.Add("En attente de sa clé publique ...");
                            CommonChat.MsgBoxStatic.Enabled = false;
                        }
                        else
                        {
                            CommonChat.MsgBoxStatic.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                CreateNewXML();
            }   
        }

        /// <summary>
        /// Crée un nouveau fichier s'il n'existe pas
        /// </summary>
        private static void CreateNewXML()
        {
            string xmlPath = "users.xml";
            XmlTextWriter writer = new XmlTextWriter(xmlPath, Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;

            writer.WriteStartElement("friends");
            writer.WriteElementString("myport", "5053");
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();

            GetFriends();
        }

        /// <summary>
        /// Établis une connexion avec l'ami affiché sur l'onglet actif
        /// </summary>
        public static void ConnectToFriend(string friendName)
        {
            // On va chercher de les données de l'ami dans le fichier XML
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            string currentIP = "";
            string currentPort = "";

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend")
                {
                    string name = node.Attributes[0].InnerText;
                    if (name == friendName)
                    {
                        // si on a sa clé publique, active le chat
                        if (node.ChildNodes[2].InnerText != "WAITING_FOR_KEY")
                        {
                            CommonChat.MsgBoxStatic.Enabled = true;
                        }
                        else
                        {
                            CommonChat.MsgBoxStatic.Enabled = false;
                            CommonChat.SendBtnStatic.Enabled = false;
                        }

                        // récupère les infos du contact actif
                        currentIP = node.ChildNodes[0].InnerText;
                        currentPort = node.ChildNodes[1].InnerText;
                        break;
                    }
                }
            }

            if (currentIP != "" && currentPort != "")
            {
                CommonChat.Client = new UdpClient();
                CommonChat.Client.Client.ReceiveTimeout = 1000;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(currentIP), Int32.Parse(currentPort));
                CommonChat.Client.Connect(ep);
            }
        }

        public static string GetNameByIP(IPAddress address)
        {
            // On va chercher de les données de l'ami dans le fichier XML
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.ChildNodes[0].InnerText == Convert.ToString(address))
                {
                    return node.Attributes[0].InnerText;
                }
            }
            return null;
        }

        /// <summary>
        /// Envoie un message à l'ami actif
        /// </summary>
        /// <param name="msg">Message à envoyer</param>
        /// <param name="isKeyMsg">Le message est-il l'envoi de notre clé public ou la demande de reset de clé (pour ne pas l'écrire)</param>
        public static void SendMessage(string msg, bool isKeyMsg)
        {
            if (CommonChat.TabControlStatic.TabPages.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("users.xml");
                // Valeurs de base
                string remotePublicKey = CommonChat.LocalPublicKey;
                
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.Name == "friend")
                    {
                        string name = node.Attributes[0].InnerText;
                        if (name == CommonChat.TabControlStatic.SelectedTab.Text)
                        {
                            // load his public key to encrypt message
                            remotePublicKey = node.ChildNodes[2].InnerText;
                            // Valeurs par défaut
                            int remotePort = Int32.Parse(node.ChildNodes[1].InnerText);
                            IPAddress remoteIPAddress = IPAddress.Parse(node.ChildNodes[0].InnerText);
                            break;
                        }
                    }
                }

                byte[] data = Encoding.UTF8.GetBytes(msg);
                byte[] encryptedData;

                if (isKeyMsg)
                {
                    // n'encrypte pas le message si c'est une clé
                    encryptedData = data;
                    // envoie à tous les amis
                    foreach (string friendName in GetFriendNames())
                    {
                        ConnectToFriend(friendName);
                        CommonChat.Client.Send(encryptedData, encryptedData.Length);
                        CommonChat.Client.Close();
                    }
                }
                else
                {
                    // Envoie à l'ami actif
                    ConnectToFriend(CommonChat.TabControlStatic.SelectedTab.Text);
                    encryptedData = RSATools.RSAEncrypt(data, remotePublicKey, false);
                    CommonChat.FriendsChat[CommonChat.TabControlStatic.SelectedTab.Text].Items.Add("Moi [" + DateTime.Now + "] > " + msg);
                    CommonChat.Client.Send(encryptedData, encryptedData.Length);
                    CommonChat.Client.Close();
                    CommonChat.MsgBoxStatic.Text = "";
                }

                CommonChat.MsgBoxStatic.Select();
            }
        }

        /// <summary>
        /// Ajoute un ami à la base de donnée
        /// </summary>
        /// <param name="name">Nom</param>
        /// <param name="ip">Adresse IP</param>
        /// <param name="port">Port de réception</param>
        public static void AddFriendToDB(string name, string ip, string port)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            // Ajoute le contact dans un fichier XML
            XmlNode friend = doc.CreateElement("friend");
            XmlAttribute nameElement = doc.CreateAttribute("name");
            nameElement.InnerText = name;
            friend.Attributes.Append(nameElement);
            XmlNode ipElement = doc.CreateElement("ip");
            ipElement.InnerText = ip;
            friend.AppendChild(ipElement);
            XmlNode portElement = doc.CreateElement("port");
            portElement.InnerText = port;
            friend.AppendChild(portElement);
            XmlNode publicKey = doc.CreateElement("key");
            publicKey.InnerText = "WAITING_FOR_KEY";
            friend.AppendChild(publicKey);

            doc.DocumentElement.AppendChild(friend);
            doc.Save("users.xml");

            //Create a ListView(chat) in a dictionnary which store the listview for each friend

            CommonChat.FriendsChat.Add(name, new ListBox());
            CommonChat.FriendsChat[name].Width = 606;
            CommonChat.FriendsChat[name].Height = 193;
            CommonChat.FriendsChat[name].HorizontalScrollbar = true;
            // Add a tabpage for each friend of the database
            CommonChat.TabControlStatic.TabPages.Add(name);
            // Add the ListView in the TabPage created
            CommonChat.TabControlStatic.TabPages[CommonChat.TabControlStatic.TabCount - 1].Controls.Add(CommonChat.FriendsChat[name]);

            CommonChat.FriendsChat[name].Items.Clear();
            CommonChat.FriendsChat[name].Items.Add("Votre ami doit vous rajouter dans sa liste de contacts lorsque vous êtes connecté afin de vous transmettre sa clé publique.");
            CommonChat.FriendsChat[name].Items.Add("");
            CommonChat.FriendsChat[name].Items.Add("En attente de sa clé publique ...");

            // Envoie notre clé publique
            SendMessage(CommonChat.LocalPublicKey, true);

            MessageBox.Show(new Form() { TopMost = true }, name + " a bien été ajouté à votre liste de contacts.", "Succès !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Vérifie s'il n'y a pas collision de nom entre le nouveau contact et les contacts existants
        /// </summary>
        /// <param name="name">Nom du nouveau contact</param>
        /// <param name="ip">IP du nouveau contact</param>
        /// <returns>Bool</returns>
        public static bool IsNewFriendValid(string name, string ip)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend")
                {
                    if (node.Attributes[0].InnerText == name)
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "Un contact possédant ce nom existe déjà.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            if (child.Name == "ip" && child.InnerText == ip)
                            {
                                MessageBox.Show(new Form() { TopMost = true }, "Un contact possédant cette IP existe déjà.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Vérifie s'il n'y a pas collision de nom entre le contact modifié et les contacts existants
        /// </summary>
        /// <param name="name">Nom du nouveau contact</param>
        /// <param name="ip">IP du nouveau contact</param>
        /// <returns>Bool</returns>
        public static bool IsModifiedFriendValid(string oldName, string name, string ip)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend" && node.Attributes[0].InnerText != oldName)
                {
                    if (node.Attributes[0].InnerText == name)
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "Un contact possédant ce nom existe déjà.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            if (child.Name == "ip" && child.InnerText == ip)
                            {
                                MessageBox.Show(new Form() { TopMost = true }, "Un contact possédant cette IP existe déjà.", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Enlève un ami de la base de donnnée et du TabControl
        /// </summary>
        /// <param name="friendName"></param>
        public static void RemoveFriend(string friendName)
        {
            SendMessage("RESET_KEY", true);
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");
            XmlNode nodeRemoved = doc.SelectSingleNode("//friend[@name='" + friendName + "']");
            nodeRemoved.ParentNode.RemoveChild(nodeRemoved);
            doc.Save("users.xml");

            CommonChat.FriendsChat.Remove(friendName);
            // Détruit l'onglet actuel
            CommonChat.TabControlStatic.TabPages.Remove(CommonChat.TabControlStatic.SelectedTab);
        }

        /// <summary>
        /// Modifie les informations du contact dans la BD
        /// </summary>
        /// <param name="newName">New name</param>
        /// <param name="newIP">New IP</param>
        /// <param name="newPort">New port</param>
        public static void ModifyFriend(string oldName, string newName, string newIP, string newPort)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend" && node.Attributes[0].InnerText == oldName)
                {
                    node.Attributes[0].InnerText = newName;

                    // Si on change d'IP, reset la clé public
                    if (node.ChildNodes[0].InnerText != newIP)
                    {
                        CommonChat.FriendsChat[oldName].Items.Clear();
                        CommonChat.FriendsChat[oldName].Items.Add("Votre ami doit vous rajouter dans sa liste de contacts lorsque vous êtes connecté afin de vous transmettre sa clé publique.");
                        CommonChat.FriendsChat[oldName].Items.Add("");
                        CommonChat.FriendsChat[oldName].Items.Add("En attente de sa clé publique ...");
                        CommonChat.MsgBoxStatic.Enabled = false;
                        node.ChildNodes[2].InnerText = "WAITING_FOR_KEY";
                    }

                    node.ChildNodes[0].InnerText = newIP;
                    node.ChildNodes[1].InnerText = newPort;
                    CommonChat.TabControlStatic.SelectedTab.Text = newName;
                    break;
                }
            }
            
            doc.Save("users.xml");
        }

        /// <summary>
        /// Renvoie True si on a accès à la clé publique de l'interlocuteur
        /// </summary>
        /// <returns></returns>
        public static bool HasKey(string friendName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                // Séléctionne l'ami actuel
                if (node.Name == "friend" && node.Attributes[0].InnerText == friendName)
                {
                    // Si on change d'IP, reset la clé public
                    if (node.ChildNodes[2].InnerText != "WAITING_FOR_KEY")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Renvoie tous les noms d'amis ajoutés
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFriendNames()
        {
            List<string> result = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                // Séléctionne l'ami actuel
                if (node.Name == "friend")
                {
                    result.Add(node.Attributes[0].InnerText);
                }
            }

            return result;
        }

        /// <summary>
        /// Remplace une clé publique d'un contact
        /// </summary>
        /// <param name="friendName">Nom de l'ami concerné</param>
        /// <param name="newKey">Nouvelle clé</param>
        public static void SetKey(string friendName, string newKey)
        {
            string name;
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node.Name == "friend" && node.Attributes[0].InnerText == friendName)
                {
                    name = node.Attributes[0].InnerText;
                    node.ChildNodes[2].InnerText = newKey;

                    if (newKey != "WAITING_FOR_KEY")
                    {
                        if (!HasKey(friendName))
                        {
                            CommonChat.FriendsChat[name].Items.Clear();
                            CommonChat.FriendsChat[name].Items.Add("Clé publique récupérée, vous pouvez discuter !");
                            SendMessage(CommonChat.LocalPublicKey, true);
                            CommonChat.MsgBoxStatic.Enabled = true;
                        }
                    }
                    else
                    {
                        CommonChat.FriendsChat[name].Items.Clear();
                        CommonChat.FriendsChat[name].Items.Add("Votre ami s'est déconnecté et / ou a fermé son programme. En attente de l'envoi de sa nouvelle clé publique.");
                        CommonChat.MsgBoxStatic.Enabled = false;
                    }
                    break;
                }
            }
            doc.Save("users.xml");
        }
    }
}