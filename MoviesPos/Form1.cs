using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace MoviesPos
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos");
        int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = 0;
            string userType = "";
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from users_table where username = '" + bunifuMetroTextbox1.Text +
                "'and password = '" + bunifuMetroTextbox2.Text + "'";
            command.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            i = Convert.ToInt32(dataTable.Rows.Count.ToString());

            string selectQuery = "select * from users_table WHERE username=" + bunifuMetroTextbox1.Text;
            MySqlDataReader mdr;
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
               userType = mdr.GetString("user_type");
            }

            if (userType == "CASHIER")
            {
                MoviePOS mainform = new MoviePOS();
                this.Hide();
                mainform.ShowDialog();
            } else if(userType == "ADMIN")
            {
                Admin mainform = new Admin();
                this.Hide();
                mainform.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }
            this.Show();
            connection.Close();
        }
        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

    }
}
