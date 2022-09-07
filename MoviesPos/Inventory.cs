using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MoviesPos
{
    public partial class Inventory : Form
    {
        
        string Connection = "datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos";
        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bunifuCustomDataGrid2.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
                textBox4.Text = row.Cells[7].Value.ToString();
            }
        }
        private Admin mainForm = null;
        public Inventory(Form callingForm)
        {
            mainForm = callingForm as Admin;
            InitializeComponent();
        }
        private void RefreshTable()
        {
            try
            {
                string Query = "select barcode as 'Product No.', product_name as 'Name', product_type as 'Type',stock_in as 'Stocks in',stock as 'Stocks',stock_out as 'Stocks out' ,product_cost as 'Product Cost',product_price as 'Price' from tyangge;";
                MySqlConnection MyConn = new MySqlConnection(Connection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                bunifuCustomDataGrid2.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void Inventory_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into tyangge(barcode, product_name, product_type,stock_in ,stock,stock_out,product_cost, product_price) values('"
                    + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.comboBox1.Text + "','" + this.textBox5.Text + "','"
                    + this.textBox3.Text + "','" + 0 + "','" +this.textBox6.Text + "','" +this.textBox4.Text + "');";

                MySqlConnection MyConn = new MySqlConnection(Connection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                MySqlDataReader MyReader;
                MyConn.Open();
                MyReader = MyCommand.ExecuteReader();
                while (MyReader.Read())
                {
                }
                MyConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                string Query = "update tyangge set product_name ='" +
                    textBox2.Text + "',product_type ='" + comboBox1.Text 
                    + "',stock_in ='" + textBox5.Text
                    + "',stock ='" + textBox3.Text
                    + "',product_cost ='" + textBox6.Text
                    + "',product_price ='" + textBox4.Text 
                    + "'where barcode ='" + textBox1.Text + "';";
                MySqlConnection MyConn = new MySqlConnection(Connection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                MySqlDataReader MyReader;
                MyConn.Open();
                MyReader = MyCommand.ExecuteReader();
                MessageBox.Show("Product Updated");
                while (MyReader.Read())
                {
                }
                MyConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            RefreshTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.bunifuCustomDataGrid2.SelectedRows)
                {
                    using (MySqlConnection connection = new MySqlConnection(Connection))
                    {
                        MySqlCommand command = connection.CreateCommand();
                        string name = bunifuCustomDataGrid2.SelectedRows[0].Cells[1].Value.ToString();
                        command.CommandText = "Delete from tyangge where product_name='" + name + "'";

                        bunifuCustomDataGrid2.Rows.RemoveAt(this.bunifuCustomDataGrid2.SelectedRows[0].Index);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                }
            }               
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(Connection);
            connect.Open();
            string selectQuery = "select * from tyangge WHERE product_name ='" + textBox2.Text+ "'";
            MySqlCommand command = new MySqlCommand(selectQuery, connect);
            MySqlDataReader mdr;
            mdr = command.ExecuteReader();
            if (mdr.Read())
            {
                textBox1.Text = mdr.GetString("barcode");
                comboBox1.Text = mdr.GetString("product_type");
                textBox5.Text = mdr.GetString("stock_in");
                textBox3.Text = mdr.GetString("stock");
                textBox6.Text = mdr.GetString("product_cost");
                textBox4.Text = mdr.GetString("product_price");

            }
            else
            {
                MessageBox.Show("No Snacks Found in Tyangge");
            }
            connect.Close();
        }

        private void bunifuCustomLabel9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
