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
    public partial class Tickets : Form
    {
        string connect = "datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos";

        private Admin mainForm = null;
        public Tickets(Form callingForm)
        {
            mainForm = callingForm as Admin;
            InitializeComponent();
        }
        private void RefreshTable()
        {
            try
            {
                string Query = "select ticket_number as 'Ticket No.', name as 'Movie', discount_type as ' Discount',room_number as 'Room', ticket_date as 'Date', snack as 'Snacks', snack_price as 'Snack Price',ticket_price as 'Ticket Price' from ticket_table;";
                MySqlConnection MyConn = new MySqlConnection(connect);
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

        private void Tickets_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow row in this.bunifuCustomDataGrid2.SelectedRows)
                {
                    using (MySqlConnection connection = new MySqlConnection(connect))
                    {
                        MySqlCommand command = connection.CreateCommand();
                        int ticket_number = Convert.ToInt32(bunifuCustomDataGrid2.SelectedRows[0].Cells[0].Value);
                        command.CommandText = "Delete from ticket_table where ticket_number='" + ticket_number + "'";

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
        private void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "select ticket_number as 'Ticket No.', name as 'Movie', discount_type as 'Discount',room_number as 'Room' ,ticket_date as 'Date', snack as 'Snacks Bought', snack_price as 'Snacks Cost',ticket_price as 'Ticket Cost' from ticket_table where ticket_number like '%" + bunifuMetroTextbox1.Text + "%'";
                MySqlConnection MyConn = new MySqlConnection(connect);
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
    }
}
