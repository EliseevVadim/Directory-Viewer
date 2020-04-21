using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<string> names = new List<string>();
        List<string> files = new List<string>();
        List<string> tofile = new List<string>();
        List<string> namesfromitems = new List<string>();
        List<Color> mas = new List<Color>();
        Font currentfont;
        public Form1()
        {
            InitializeComponent();
            SendColors.sC = new SendColors.SC(this.Reload);
            SendFont.sF = new SendFont.SF(this.CatchFont);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        void Reload(List<Color> colors)
        {
            mas = colors;
            foreach (ListViewItem item in listView1.Items)
            {
                item.BackColor = GetColor(item.SubItems[2].Text);
            }
        }
        void CatchFont(string s, int N)
        {
            Font font = new Font(s, N);
            currentfont = font;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Font = font;
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }            
        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Hide();
            progressBar1.Hide();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    listView1.Items.Clear();
                    treeView1.Nodes.Clear();
                    toolStripStatusLabel1.Text = string.Empty;
                    toolStripStatusLabel2.Text = string.Empty;
                    chart1.Series[0].Points.Clear();      
                    var root = Path.GetFullPath(folderBrowserDialog.SelectedPath);
                    treeView1.Nodes.Add(GetDirNode(root));
                }
                catch(UnauthorizedAccessException)
                {
                    MessageBox.Show("Access Error");
                }
        }
            

        }
        private static TreeNode GetDirNode(string s)
        {
            var dirnode = new TreeNode(Path.GetFullPath(s));
            foreach (string dirs in Directory.GetDirectories(Path.GetFullPath(s)))
            {
                dirnode.Nodes.Add(GetDirNode(dirs));
            }
            foreach (var file in Directory.GetFiles(Path.GetFullPath(s)))
            {
                dirnode.Nodes.Add(new TreeNode(file));
            }
            return dirnode;
        }
                                       
        private void button1_Click_1(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click_1(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter="Текстовые документы(*.txt)|*.txt";
            saveFileDialog.InitialDirectory = @"C:\Users\User\Desktop";
            saveFileDialog.FileName = "test.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(saveFileDialog.FileName);
                foreach (string s in tofile)
                {
                    writer.WriteLine(s);
                }
                writer.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HelpForm form = new HelpForm();
            form.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripStatusLabel1.Text = string.Empty;
            toolStripStatusLabel2.Text = string.Empty;
            string s;
            tofile.Clear();
            listView1.Items.Clear();
            names.Clear();
            files.Clear();
            namesfromitems.Clear();
            ulong totalbytes = 0;
            List<string> fl = GetFileList(GetList(treeView1.SelectedNode));
            fl.Reverse();
            label2.Show();
            progressBar1.Value = 0;
            progressBar1.Maximum = files.Count;
            label2.Show();
            progressBar1.Show();
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                ListViewItem item = new ListViewItem(fileInfo.Name);
                item.SubItems.Add(fileInfo.Length.ToString());
                totalbytes +=(ulong)fileInfo.Length;
                try
                {
                    item.SubItems.Add(fileInfo.Extension.Remove(0, 1));
                    s = fileInfo.Extension.Remove(0, 1);
                    tofile.Add($"{fileInfo.Name} {fileInfo.Length.ToString()} {fileInfo.Extension.Remove(0, 1)}");
                }
                catch
                {
                    item.SubItems.Add(fileInfo.Extension);
                    s = fileInfo.Extension;
                    tofile.Add($"{fileInfo.Name}\t\t{fileInfo.Length.ToString()}\t\t{fileInfo.Extension}");
                }
                item.Checked = true;
                item.BackColor = GetColor(s);
                item.Font = currentfont;
                listView1.Items.Add(item);
                progressBar1.Value++;
            }
            toolStripStatusLabel1.Text = "Total bytes: " + totalbytes+"                      ";
            label2.Hide();
            progressBar1.Hide();
        }
        private List<string> GetList (TreeNode node)
        {
            names.Add(node.Text);
            foreach (TreeNode tn in node.Nodes)
            {
                GetList(tn);
            }
            return names;
        }
        private List<string> GetFileList(List<string> all)
        {
            foreach (string s in all)
            {
                if (File.Exists(s))
                {
                    files.Add(s);
                }
                else if (Directory.Exists(s))
                {

                }
            }
            return files;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                int shortcounter = 0;
                int middlecounter = 0;
                int longcounter = 0;
                namesfromitems.Clear();
                int counter = 0;
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Checked)
                    {
                        counter++;
                        namesfromitems.Add(item.Text);
                    }
                }
                toolStripStatusLabel2.Text = counter + " of " + listView1.Items.Count + " items selected";
                foreach(string s in namesfromitems)
                {
                    if (s.Length <= 10)
                    {
                        shortcounter++;
                    }
                    else if (s.Length > 10 && s.Length <= 15)
                    {
                        middlecounter++;
                    }
                    else
                    {
                        longcounter++;
                    }
                }
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY("Short length files", shortcounter);
                chart1.Series[0].Points.AddXY("Middle length files", middlecounter);
                chart1.Series[0].Points.AddXY("Long length files", longcounter);
            }
            catch
            {

            }
        }
        private Color GetColor(string s) 
        {
            if (mas.Count == 0)
            {
                switch (s)
                {                    
                    case "png":
                    case "jpg":
                    case "bmp":
                    case "gif":
                        return Color.Green;
                    case "doc":
                    case "docx":
                    case "pptx":
                    case "pdf":
                    case "txt":
                    case "xlsx":
                    case "xlsm":
                    case "xls":
                        return Color.Cyan;
                    case "rar":
                    case "zip":
                    case "7z":
                        return Color.Magenta;
                    case "exe":
                    case "dll":
                        return Color.CadetBlue;
                    default:
                        return Color.Empty;
                }
            }
            else
            {
                switch (s)
                {
                    case "png":
                    case "jpg":
                    case "bmp":
                    case "gif":
                        return mas[0];
                    case "doc":
                    case "docx":
                    case "pptx":
                    case "pdf":
                    case "txt":
                    case "xlsx":
                    case "xlsm":
                    case "xls":
                        return mas[1];
                    case "rar":
                    case "zip":
                    case "7z":
                        return mas[2];
                    case "exe":
                    case "dll":
                        return mas[3];
                    default:
                        return Color.Empty;
                }
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorChange colorChange = new ColorChange();
            colorChange.Show();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontChange fontChange = new FontChange();
            fontChange.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show();
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontToolStripMenuItem_Click(sender, e);
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorToolStripMenuItem_Click(sender, e);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }
    }
}
