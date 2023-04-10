using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ookplab3
{
    public partial class Form2 : Form
    {
        internal Facade F = new Facade();
        internal DataGridView Dgv1;
        public Form2(DataGridView Dgv)
        {
            Dgv1 = Dgv;
            InitializeComponent();

            F.AutoComplete(Dgv1, textBox1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F.AddItem(Dgv1, textBox1.Text, dateTimePicker1.Value, numericUpDown1.Value, numericUpDown2.Value,
                numericUpDown3.Value, numericUpDown4.Value, numericUpDown5.Value, textBox2.Text, textBox3.Text);
        }
    }
}
