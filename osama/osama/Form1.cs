using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace osama
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data SOurce=DATATEC; Initial Catalog=milestone_project; Integrated Security=true");
          
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data SOurce=DATATEC; Initial Catalog=milestone_project; Integrated Security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("reserve_time", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@st_date",SqlDbType.Date);
            cmd.Parameters.Add("@en_date", SqlDbType.Date);
            cmd.Parameters.Add("@st_time", SqlDbType.Time);
            cmd.Parameters.Add("@en_time", SqlDbType.Time);
            cmd.Parameters["@st_date"].Value = dateTimePicker1.Value.Date;
            cmd.Parameters["@en_date"].Value = dateTimePicker2.Value.Date;
            cmd.Parameters["@st_time"].Value = dateTimePicker3.Value.TimeOfDay;
            cmd.Parameters["@en_time"].Value = dateTimePicker4.Value.TimeOfDay;

            //cmd.ExecuteNonQuery();
           
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable table = new DataTable();
             da.Fill(table);
             dataGridView1.DataSource = table;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
        int i = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            i++;
            textBox1.Visible = true;
            if (i == 2)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("reserve", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@st_date", SqlDbType.Date);
                cmd.Parameters.Add("@en_date", SqlDbType.Date);
                cmd.Parameters.Add("@st_time", SqlDbType.Time);
                cmd.Parameters.Add("@en_time", SqlDbType.Time);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters.Add("@@res_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@@fee", SqlDbType.Money).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@roomid", SqlDbType.Int);
                cmd.Parameters["@st_date"].Value = dateTimePicker1.Value.Date;
                cmd.Parameters["@en_date"].Value = dateTimePicker2.Value.Date;
                cmd.Parameters["@st_time"].Value = dateTimePicker3.Value.TimeOfDay;
                cmd.Parameters["@en_time"].Value = dateTimePicker4.Value.TimeOfDay;
                cmd.Parameters["@id"].Value = Convert.ToInt32(textBox1.Text);
                cmd.Parameters["@roomid"].Value = Convert.ToInt32(textBox2.Text);
                cmd.ExecuteNonQuery();
                string n = cmd.Parameters["@@res_id"].Value.ToString();
                 
               SqlCommand cmds = new SqlCommand("fee_cal", con);
                cmds.CommandType = CommandType.StoredProcedure;
                cmds.Parameters.Add("@res_id", SqlDbType.Int);
                cmds.Parameters.Add("@@x", SqlDbType.Money).Direction = ParameterDirection.Output;
                cmds.Parameters["@res_id"].Value = Convert.ToInt32(n);
                cmds.ExecuteNonQuery();
                string t = cmds.Parameters["@@x"].Value.ToString();
                MessageBox.Show("Your Reservation ID is " + n + "\nYour Fees is " + t);
                con.Close();
            }
        }
    }
}
