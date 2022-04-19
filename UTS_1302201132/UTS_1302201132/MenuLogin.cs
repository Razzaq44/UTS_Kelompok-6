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

namespace UTS_1302201132
{
    public partial class MenuLogin : Form
    {
        public MenuLogin()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection konek = new SqlConnection(@"Data Source=RARA;Initial Catalog=E-Wallet;Integrated Security=True");
            SqlDataAdapter adr = new SqlDataAdapter("select count (*) from DataLogin where Username = '" + textusername.Text + "' and Password = '" + textpassword.Text + "'", konek);
            DataTable dt = new DataTable();

            adr.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                MenuBeranda panggil = new MenuBeranda();
                panggil.Show();
            }
            else
            {
                MessageBox.Show("username atau password anda salah", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
