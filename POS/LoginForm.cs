
using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string pass = textBox2.Text;
            string query = $"select * from users where email='{email}' and password='{pass}'";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString)) 
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.HasRows)
                    {
                        MessageBox.Show("Login Successful","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }   
                    else
                    {
                        MessageBox.Show("Wrong Credentials!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
