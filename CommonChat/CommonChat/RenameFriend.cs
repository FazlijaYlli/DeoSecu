/*
 * Lieu        : ETML
 * Auteur      : Ylli Fazlija & Hugo Ducommun
 * Date        : 02.03.2020
 * Description : Chat sécurisé / Classe ModifyFriend
 */

using System;
using System.Windows.Forms;

namespace CommonChat
{
    public partial class RenameFriend : Form
    {
        public RenameFriend()
        {
            InitializeComponent();
            nameTextBox.Text = Database.GetCurrentFriend()[0];
            ipTextBox.Text = Database.GetCurrentFriend()[1];
            portTextBox.Text = Database.GetCurrentFriend()[2];
            ipTextBox.Enabled = false;
            portTextBox.Enabled = false;
        }

        /// <summary>
        /// Modifie l'ami dans la base de donnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            if (CommonChat.TabControlStatic.TabPages.Count != 0)
            {
                if (nameTextBox.Text != String.Empty && ipTextBox.Text != String.Empty && portTextBox.Text != String.Empty)
                {
                    string oldName = CommonChat.TabControlStatic.SelectedTab.Text;

                    if (Database.IsModifiedFriendValid(oldName, nameTextBox.Text, ipTextBox.Text))
                    {
                        DialogResult result = MessageBox.Show("Voulez-vous vraiment appliquer les modifications ?", "Modification d'ami", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            Database.RenameFriend(oldName, nameTextBox.Text);
                            this.Close();
                        }
                    }
                    else
                    {
                        // quitte la fonction
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "Veuillez remplir tous les champs !", "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(new Form() { TopMost = true }, "Vous n'avez pas d'onglet de contact ouvert.\n Veuillez créer un contact pour le modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Si l'utilisateur appuie sur enter, appuie sur le bouton Modifier
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
