using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MoviesPos
{
    public partial class Admin : Form
    {
        string Connection = "datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos";
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void RefreshTable()
        {
            try
            {
                string Query = "select * from movie_table;";
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

        private void Admin_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in this.bunifuCustomDataGrid2.SelectedRows)
                {
                    using (MySqlConnection connection = new MySqlConnection(Connection))
                    {
                        MySqlCommand command = connection.CreateCommand();
                        string name = bunifuCustomDataGrid2.SelectedRows[0].Cells[1].Value.ToString();
                        command.CommandText = "Delete from movie_table where Movie='" + name + "'";

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
            RefreshTable();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into movie_table(Movie, Room, Price, Duration, Time, Genre) values('"
                    + this.bunifuMetroTextbox1.Text + "','" + this.bunifuMetroTextbox2.Text + "','" + this.bunifuMetroTextbox6.Text + "','"
                    + this.bunifuMetroTextbox3.Text + "','" + this.bunifuMetroTextbox4.Text + "','" + this.bunifuMetroTextbox5.Text + "');";

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
            label8.Text = "";
            bunifuMetroTextbox1.Text = "";
            bunifuMetroTextbox2.Text = "";
            bunifuMetroTextbox6.Text = "";
            bunifuMetroTextbox3.Text = "";
            bunifuMetroTextbox4.Text = "";
            bunifuMetroTextbox5.Text = "";
            RefreshTable();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                string Query = "update movie_table set Movie ='" + bunifuMetroTextbox1.Text + "',Room ='" +
                    bunifuMetroTextbox2.Text + "',Price ='" + bunifuMetroTextbox6.Text
                    + "',Duration ='" + bunifuMetroTextbox3.Text + "',Time ='" +
                    bunifuMetroTextbox4.Text + "',Genre ='" + bunifuMetroTextbox5.Text
                    + "'where Movie ='"+ bunifuMetroTextbox1.Text +"';";
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
        private void button3_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(this);
            inventory.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Tickets inventory = new Tickets(this);
            inventory.Show();
        }


        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 )
            {
                DataGridViewRow row = this.bunifuCustomDataGrid2.Rows[e.RowIndex];
                label8.Text = row.Cells[0].Value.ToString();
                bunifuMetroTextbox1.Text = row.Cells[1].Value.ToString();
                bunifuMetroTextbox2.Text = row.Cells[2].Value.ToString();
                bunifuMetroTextbox6.Text = row.Cells[3].Value.ToString();
                bunifuMetroTextbox3.Text = row.Cells[4].Value.ToString();
                bunifuMetroTextbox4.Text = row.Cells[5].Value.ToString();
                bunifuMetroTextbox5.Text = row.Cells[6].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid2.SelectedRows.Count == 1)
            {
                string label = bunifuCustomDataGrid2.SelectedRows[0].Cells[0].Value + string.Empty;
                string movie_name = bunifuCustomDataGrid2.SelectedRows[0].Cells[1].Value + string.Empty;
                string room = bunifuCustomDataGrid2.SelectedRows[0].Cells[2].Value + string.Empty;
                string price = bunifuCustomDataGrid2.SelectedRows[0].Cells[3].Value + string.Empty;
                string duration = bunifuCustomDataGrid2.SelectedRows[0].Cells[4].Value + string.Empty;
                string time = bunifuCustomDataGrid2.SelectedRows[0].Cells[5].Value + string.Empty;
                string genre = bunifuCustomDataGrid2.SelectedRows[0].Cells[6].Value + string.Empty;
                label8.Text = label;
                bunifuMetroTextbox1.Text = movie_name;
                bunifuMetroTextbox2.Text = room;
                bunifuMetroTextbox6.Text = price;
                bunifuMetroTextbox3.Text = duration;
                bunifuMetroTextbox4.Text = time;
                bunifuMetroTextbox5.Text = genre;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            bunifuMetroTextbox1.Text = "";
            bunifuMetroTextbox2.Text = "";
            bunifuMetroTextbox6.Text = "";
            bunifuMetroTextbox3.Text = "";
            bunifuMetroTextbox4.Text = "";
            bunifuMetroTextbox5.Text = "";
            label8.Text = "";
        }
    }
}
