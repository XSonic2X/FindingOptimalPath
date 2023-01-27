using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Map map = new Map(10,10,3);
            label1.Text = map.Set(new Coordinates(0, 0), new Coordinates(9, 9)).Set().Set(dataGridView1).ToString();
        }
    }
}
