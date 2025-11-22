using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
            LoadRoles();
            LoadUser();
        }

        private void LoadRoles()
        {

            comboBoxAdminRole.Items.Clear();
            comboBoxAdminRole.Items.Add("Admin");
            comboBoxAdminRole.Items.Add("Doctor");
            comboBoxAdminRole.Items.Add("User");

            if (comboBoxAdminRole.Items.Count > 0)
            {
                comboBoxAdminRole.SelectedIndex = 0;
            }
        }

        private void LoadUser()
        {
            try
            {
                UserDAO userDAO = new UserDAO();
                List<User> userList = userDAO.GetAll();

                if (userList == null || userList.Count == 0)
                {
                    MessageBox.Show("Aucun utilisateur trouvé.", "Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dataGridViewListUser.DataSource = null;
                    return;
                }

                var displayList = userList.Select(u => new
                {
                    Id = u.Id,
                    Firstname = u.Firstname,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    Role = u.Role
                }).ToList();

                this.dataGridViewListUser.DataSource = null;
                this.dataGridViewListUser.DataSource = displayList;
                this.dataGridViewListUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des utilisateurs: {ex.Message}",
                               "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdminAdd_Click(object sender, EventArgs e)
        {
            CreateUser();
        }

        private void CreateUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxAdminFirstname.Text))
                {
                    MessageBox.Show("Le prénom est obligatoire.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminFirstname.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxAdminName.Text))
                {
                    MessageBox.Show("Le nom est obligatoire.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxAdminEmail.Text))
                {
                    MessageBox.Show("L'email est obligatoire.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminEmail.Focus();
                    return;
                }

                if (!textBoxAdminEmail.Text.Contains("@") || !textBoxAdminEmail.Text.Contains("."))
                {
                    MessageBox.Show("Le format de l'email n'est pas valide.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminEmail.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxAdminPassword.Text))
                {
                    MessageBox.Show("Le mot de passe est obligatoire.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminPassword.Focus();
                    return;
                }

                if (textBoxAdminPassword.Text.Length < 6)
                {
                    MessageBox.Show("Le mot de passe doit contenir au moins 6 caractères.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAdminPassword.Focus();
                    return;
                }

                if (comboBoxAdminRole.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner un rôle.", "Validation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxAdminRole.Focus();
                    return;
                }

                bool roleValue = ConvertRoleToBool(comboBoxAdminRole.SelectedItem.ToString());

                User newUser = new User(
                    0,
                    textBoxAdminFirstname.Text.Trim(),
                    textBoxAdminName.Text.Trim(),
                    textBoxAdminPassword.Text.Trim(),
                    textBoxAdminEmail.Text.Trim(),
                    roleValue
                );

                UserDAO userDAO = new UserDAO();
                bool success = userDAO.Add(newUser);

                if (success)
                {
                    MessageBox.Show("Utilisateur ajouté avec succès!", "Succès",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();
                    LoadUser();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de l'utilisateur.", "Erreur",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ConvertRoleToBool(string role)
        {
            // Convertir le rôle string en bool
            // true = Admin, false = autres rôles
            return role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        private void buttonAdminDelete_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void DeleteUser()
        {
            try
            {
                if (dataGridViewListUser.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.", "Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int userId = Convert.ToInt32(dataGridViewListUser.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet utilisateur ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UserDAO userDAO = new UserDAO();
                    bool success = userDAO.Delete(userId);

                    if (success)
                    {
                        MessageBox.Show("Utilisateur supprimé avec succès!", "Succès",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUser();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression.", "Erreur",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdminClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            textBoxAdminFirstname.Clear();
            textBoxAdminName.Clear();
            textBoxAdminEmail.Clear();
            textBoxAdminPassword.Clear();

            if (comboBoxAdminRole.Items.Count > 0)
            {
                comboBoxAdminRole.SelectedIndex = 0;
            }

            textBoxAdminFirstname.Focus();
        }
    }
}