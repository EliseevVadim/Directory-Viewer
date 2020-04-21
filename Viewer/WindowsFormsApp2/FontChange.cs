using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class FontChange : Form
    {
        public FontChange()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FontChange_Load(object sender, EventArgs e)
        {
            foreach(FontFamily item in FontFamily.Families)
            {
                comboBox1.Items.Add(item.Name);                
            }
            for (int i=1; i<=72; i++)
            {
                comboBox2.Items.Add(i);
            }            
            comboBox1.SelectedItem = comboBox1.Items[134];            
            comboBox2.SelectedItem = comboBox2.Items[7];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                string font = comboBox1.SelectedItem.ToString();
                int size = int.Parse(comboBox2.SelectedItem.ToString());
                SendFont.sF(font, size);
                Close();
            }
            catch
            {
                MessageBox.Show("Fill all suggestions");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
