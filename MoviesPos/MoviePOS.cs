using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
namespace MoviesPos
{
    public partial class MoviePOS : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos");
        string MyConnection = "datasource=localhost;port=3306;username=root;password='';Initial Catalog=movie_pos";
        public MoviePOS()
        {
            InitializeComponent();
        }
        //Tyangge Button
        private void MoviePOS_Load(object sender, EventArgs e)
        {
            DisplayTransaction();
            try
            {
                string Query = "select * from movie_table; ";
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                bunifuCustomDataGrid1.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Tyangge tyangge = new Tyangge(this);

            tyangge.Show();
        }
        //Ticket number Refresh
        private void DisplayTransaction()
        {
            connection.Open();
            string selectQuery = "select max(ticket_number)+1 as 'ticket_barcode' from ticket_table ";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ticket = reader.GetString("ticket_barcode");
                textBox13.Text = ticket;
            }
            command.Dispose();
            reader.Close();
            connection.Close();
            textBox10.Text = "0";
            textBox9.Text = "0";
            textBox11.Text = "0";

        }

        private void bunifuCustomDataGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
        //Add to Ticket
        private void button4_Click_1(object sender, EventArgs e)
        {
            double discount ;
            string name ;
            switch (comboBox1.Text)
            {
                case "Student":
                    discount = .15;
                    name = " (15%)";
                    break;
                case "Senior":
                    discount = .25;
                    name = " (25%)";
                    break;
                case "VIP":
                    discount = .35;
                    name = " (35%)";
                    break;
                default:
                    discount = 0;
                    name = " ( No Discount)";
                    break;
            }

            if (bunifuCustomDataGrid1.SelectedRows.Count == 1)
            {
                string movie_name = bunifuCustomDataGrid1.SelectedRows[0].Cells[1].Value + string.Empty;
                string room = bunifuCustomDataGrid1.SelectedRows[0].Cells[2].Value + string.Empty;
                string price = bunifuCustomDataGrid1.SelectedRows[0].Cells[3].Value + string.Empty;
                string time = bunifuCustomDataGrid1.SelectedRows[0].Cells[4].Value + string.Empty;
                string duration = bunifuCustomDataGrid1.SelectedRows[0].Cells[5].Value + string.Empty;
                textBox2.Text = movie_name;
                textBox5.Text = room;
                textBox11.Text = price;
                textBox3.Text = time;
                textBox1.Text = duration;
                textBox6.Text = comboBox1.Text + name;
                textBox9.Text = (double.Parse(textBox11.Text) * discount).ToString(); ;
            }


        }
        //Tyangge Add to Ticket
        public string TextBox4
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public string TextBox8
        {
            get { return textBox8.Text; }
            set { textBox8.Text = value; }
        }
        public string TextBox10
        {
            get { return textBox10.Text; }
            set { textBox10.Text = value; }
        }
        //Calculate Total
        private void button7_Click(object sender, EventArgs e)
        {
            double total;
            total = (double.Parse(textBox11.Text) - double.Parse(textBox9.Text) + double.Parse(textBox10.Text));
            textBox7.Text = total.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into ticket_table(ticket_number, name, discount_type, room_number, ticket_date, snack, snack_price, ticket_price) values('"
                    + this.textBox13.Text + "','" + this.textBox2.Text +"','"+this.textBox6.Text + "','" + this.textBox5.Text + "','"
                    + this.dateTimePicker1.Text + "','" + this.textBox4.Text +", "+ this.textBox8.Text + "','" + this.textBox10.Text + "','"
                    + this.textBox7.Text + "');";

                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                MySqlDataReader MyReader;
                MyConn.Open();
                MyReader = MyCommand.ExecuteReader();
                MessageBox.Show("Ticket Saved");
                while (MyReader.Read())
                {
                }
                MyConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
            textBox5.Clear();
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox6.Clear();
            textBox4.Clear();
            textBox8.Clear();
            textBox11.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox7.Clear();
            DisplayTransaction();

        }       
        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox3.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox6.Clear();
            textBox4.Clear();
            textBox8.Clear();
            textBox11.Clear();
            textBox9.Clear(); 
            textBox10.Clear();
            textBox7.Clear();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "select Movie_ID as 'Movie ID', Movie as 'Movie', Room as 'Room',Price as 'Price' ,Duration as 'Duration', Time as 'Time', Genre as 'Genre' from movie_table where Movie like '%" + textBox12.Text + "%'";
                MySqlConnection MyConn = new MySqlConnection(MyConnection);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                bunifuCustomDataGrid1.DataSource = dTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox8.Text = "";
            textBox10.Text = "0";
        }
        private void bunifuCustomLabel13_Click(object sender, EventArgs e)
        {

        }
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel14_Click(object sender, EventArgs e)
        {

        }



        private void bunifuCustomLabel15_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel15_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel13_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
