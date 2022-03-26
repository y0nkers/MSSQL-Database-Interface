using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CourseWork
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
            ActiveControl = labelAuthorization;
            checkBoxLocal.Checked = true;
            Program.authForm = this;
        }

        private void checkBoxLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLocal.Checked)
            {
                textBoxServer.Text = "localhost";
                textBoxServer.ReadOnly = true;
            }
            else
            {
                textBoxServer.Text = "";
                textBoxServer.ReadOnly = false;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Program.connection = @"Data Source=" + textBoxServer.Text + ";Initial Catalog=" + textBoxDatabase.Text + ";User Id=" + textBoxLogin.Text + ";Password=" + textBoxPassword.Text + ";";

            bool connected = false;
            try
            {
                SqlConnection connection = new SqlConnection(Program.connection);
                connection.Open();
                connected = true;
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Пожалуйста, проверьте правильность введённых данных.", "Ошибка входа!");
            }
            if (connected)
            {
                Program.user = textBoxLogin.Text;
                if (Program.mainForm == null) Program.mainForm = new MainForm();
                Program.mainForm.Show();
                Hide();
            }
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "Login")
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                textBoxLogin.Text = "Login";
                textBoxLogin.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "Password")
            {
                textBoxPassword.Text = "";
                textBoxPassword.UseSystemPasswordChar = true;
                textBoxPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "Password";
                textBoxPassword.UseSystemPasswordChar = false;
                textBoxPassword.ForeColor = Color.Gray;
            }
        }

        private void AuthForm_Deactivate(object sender, EventArgs e)
        {
            textBoxLogin.Text = "";
            textBoxLogin_Leave(sender, e);
            textBoxPassword.Text = "";
            textBoxPassword_Leave(sender, e);
        }
    }
}