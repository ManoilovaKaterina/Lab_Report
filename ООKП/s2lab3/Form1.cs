using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ookplab3
{
    public partial class Form1 : Form
    {
        Facade F = new Facade();
        ViewOperations VA = new ViewOperations();
        TableOperations TA = new TableOperations();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            F.Remove(this.dataGridView1, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F.Show(dataGridView1, Property.GetItemText(Property.SelectedItem), textBox2.Text);
            F.AddListBox(dataGridView1, Property);
            F.AutoComplete(dataGridView1, textBox3, 1);
            F.DisplayCount(dataGridView1, textBox3.Text, textBox4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(dataGridView1);
            form.Show();
        }
    }
}
