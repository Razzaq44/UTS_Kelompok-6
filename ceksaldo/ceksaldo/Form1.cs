﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ceksaldo
{
    public partial class Form1 : Form
    {
        public string username;
        public double Balance;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = Console.ReadLine();
            Bank newBank = new Bank(username, 200);
            MessageBox.Show("Balance: "+Balance, "username: "+username);
        }
    }
}
