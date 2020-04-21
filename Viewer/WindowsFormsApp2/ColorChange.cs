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
    public partial class ColorChange : Form
    {
        public ColorChange()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] mas = new string[4];
            List<Color> arr = new List<Color>();
            try {
                mas[0] = comboBox1.SelectedItem.ToString();
                mas[1] = comboBox2.SelectedItem.ToString();
                mas[2] = comboBox3.SelectedItem.ToString();
                mas[3] = comboBox4.SelectedItem.ToString();
                for (int i = 0; i < mas.Length; i++)
                {
                    arr.Add(Color.FromName(mas[i]));
                    
                }
                SendColors.sC(arr);
                Close();
            }
            catch
            {
                MessageBox.Show("Choose color for all file types");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ColorChange_Load(object sender, EventArgs e)
        {
            foreach(string s in GetColors())
            {
                comboBox1.Items.Add(s);
                comboBox2.Items.Add(s);
                comboBox3.Items.Add(s);
                comboBox4.Items.Add(s);
            }
            comboBox1.SelectedItem = comboBox1.Items[51];
            comboBox2.SelectedItem = comboBox1.Items[20];
            comboBox3.SelectedItem = comboBox1.Items[79];
            comboBox4.SelectedItem = comboBox1.Items[13];            
        }
        private List<string> GetColors()
        {
            List<string> colors = new List<string>();
            string[] colorNames = Enum.GetNames(typeof(KnownColor));
            foreach (string colorName in colorNames)
            {
                KnownColor knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorName);
                if (knownColor > KnownColor.Transparent)
                {
                    colors.Add(colorName);
                }
            }
            return colors;
        }
    }
}

