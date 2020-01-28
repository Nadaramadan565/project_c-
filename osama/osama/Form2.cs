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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data SOurce=DATATEC; Initial Catalog=milestone_project; Integrated Security=true");
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data SOurce=DATATEC; Initial Catalog=milestone_project; Integrated Security=true");
            con.Open();

            SqlCommand cmd = new SqlCommand("dis_reservation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value =Convert.ToInt32( textBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }
    }
}
