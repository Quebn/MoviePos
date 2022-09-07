using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;


namespace MoviesPos
{
    public partial class Tyangge : Form
    {
        string connection = "datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos";

        private MoviePOS mainForm = null;
        public Tyangge(Form callingForm)
        {
            mainForm = callingForm as MoviePOS;
            InitializeComponent();
        }
        private void Checkout()
        {
            MySqlConnection connect = new MySqlConnection(connection);
            for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
            {
                String insertQuery =
                  "call new_sales('" +
                  bunifuCustomDataGrid1.Rows[i].Cells[0].Value + "','" +
                  bunifuCustomDataGrid1.Rows[i].Cells[1].Value + "','" +
                  bunifuCustomDataGrid1.Rows[i].Cells[2].Value + "','" +
                  bunifuCustomDataGrid1.Rows[i].Cells[4].Value + "','" +
                  bunifuCustomDataGrid1.Rows[i].Cells[5].Value + "','" +
                  bunifuCustomDataGrid1.Rows[i].Cells[6].Value + "')";

                connect.Open();
                MySqlCommand command = new MySqlCommand(insertQuery, connect);
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {

                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                connect.Close();
            }
        }

            private void button2_Click(object sender, EventArgs e)
        {
            int n = bunifuCustomDataGrid1.Rows.Add();

            bunifuCustomDataGrid1.Rows[n].Cells[0].Value = textBox1.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[1].Value = textBox2.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[2].Value = comboBox1.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[3].Value = textBox5.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[4].Value = textBox3.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[5].Value = textBox4.Text;
            bunifuCustomDataGrid1.Rows[n].Cells[6].Value = double.Parse(textBox3.Text) * double.Parse(textBox4.Text);
            int sum = 0;
            for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[6].Value);
            }
            textBox7.Text = sum.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bunifuCustomDataGrid1.Rows.Clear();
            textBox7.Clear();
            bunifuCustomDataGrid1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in bunifuCustomDataGrid1.SelectedRows)
            {
                bunifuCustomDataGrid1.Rows.RemoveAt(row.Index);
                int sum = 0;
                for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[5].Value);
                }
                textBox7.Text = sum.ToString();
            }
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            List<string> snack = new List<string>();
            List<string> drink = new List<string>();
            bunifuCustomDataGrid1.SelectAll();
            foreach (DataGridViewRow row in bunifuCustomDataGrid1.SelectedRows)
            {
                if (row.Cells[2].Value.ToString() == "FOOD")
                {
                    snack.Add(row.Cells[4].Value.ToString()+" "+row.Cells[1].Value.ToString());
                }else if (row.Cells[2].Value.ToString() == "DRINK") {
                    drink.Add(row.Cells[4].Value.ToString() +" "+row.Cells[1].Value.ToString());
                }
            }
            string[] snacks = snack.ToArray();
            string[] drinks = drink.ToArray();

            this.mainForm.TextBox4 = String.Join(", ", snacks);
            this.mainForm.TextBox8 = String.Join(", ", drinks);
            this.mainForm.TextBox10 = textBox7.Text;
            Checkout();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(connection);
            connect.Open();
            string selectQuery = "select * from tyangge WHERE product_name='" + textBox2.Text + "'"; 
            MySqlCommand command = new MySqlCommand(selectQuery, connect);
            MySqlDataReader mdr;
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
                textBox1.Text = mdr.GetString("barcode");
                comboBox1.Text = mdr.GetString("product_type");
                textBox5.Text = mdr.GetString("stock");
                textBox3.Text ="1";
                textBox4.Text = mdr.GetString("product_price");

            }
            else
            {
                MessageBox.Show("No Product Data Available");
            }
            connect.Close();
        }
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void bunifuGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }



        private void Tyangge_Load(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
