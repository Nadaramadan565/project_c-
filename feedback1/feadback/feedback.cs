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
namespace feadback
{
    public partial class feedback : Form
    {
        SqlConnection con =new SqlConnection ("Data Source=.;Initial Catalog=milestone_project;Integrated Security=True");
        public feedback()
        {
          
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();

            SqlCommand cmd = new SqlCommand("feedback", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = Convert.ToInt32(this.textBox1.Text);

            cmd.Parameters.Add("@feedback", SqlDbType.NText, 100);
            cmd.Parameters["@feedback"].Value = this.textBox2.Text;

            cmd.Parameters.Add("@@feedback_id", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            con.Close();

        }
    }
}
