using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class FormAdmin : Form
    {
        private readonly UserDAO userDAO;

        public FormAdmin()
        {
            InitializeComponent();
            userDAO = new UserDAO();
            LoadRoles();
            LoadUser();
        }

        private void LoadRoles()
        {
            comboBoxAdminRole.Items.Clear();
            comboBoxAdminRole.Items.Add("Admin");
            comboBoxAdminRole.Items.Add("Doctor");
            comboBoxAdminRole.SelectedIndex = 0;
        }

        private bool ConvertRoleToBool(string role)
        {
            return role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        private void LoadUser()
        {
            try
            {
                var userList = userDAO.GetAll();

                if (userList == null || userList.Count == 0)
                {
                    dataGridViewListUser.DataSource = null;
                    return;
                }

                var displayList = userList.Select(u => new
                {
                    Id = u.Id_user,
                    Firstname = u.Firstname,
                    Name = u.Name,
                    Email = u.Email,
                    Password = "********",
                    Role = u.Role ? "Admin" : "Doctor"
                }).ToList();

                dataGridViewListUser.DataSource = displayList;
                dataGridViewListUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des utilisateurs: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdminAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxAdminFirstname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAdminName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAdminEmail.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAdminPassword.Text))
                {
                    MessageBox.Show("Tous les champs doivent être remplis.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!textBoxAdminEmail.Text.Contains("@") || !textBoxAdminEmail.Text.Contains("."))
                {
                    MessageBox.Show("L'email n'est pas valide.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool roleValue = ConvertRoleToBool(comboBoxAdminRole.SelectedItem.ToString());

                User newUser = new User(
                    0,
                    textBoxAdminFirstname.Text.Trim(),
                    textBoxAdminName.Text.Trim(),
                    textBoxAdminEmail.Text.Trim(),
                    textBoxAdminPassword.Text.Trim(),
                    roleValue
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdminDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewListUser.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(dataGridViewListUser.SelectedRows[0].Cells["Id"].Value);

                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet utilisateur ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
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
            comboBoxAdminRole.SelectedIndex = 0;
            textBoxAdminFirstname.Focus();
        }

        private void buttonFormAdminLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            var result = MessageBox.Show(
                "Êtes-vous sûr de vouloir vous déconnecter ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Hide();
                MainForm loginForm = new MainForm();
                loginForm.Show();
            }
        }
    }
}