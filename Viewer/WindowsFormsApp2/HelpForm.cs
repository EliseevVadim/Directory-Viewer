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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "\t     Вас приветсвует справка приложения Directory Viewer!"+Environment.NewLine+Environment.NewLine;
            textBox1.Text += "Данное приложение предназначено для просмотра содержимого директорий (в том числе и скрытых)."+Environment.NewLine+Environment.NewLine;
            textBox1.Text += "Вы можете открыть директорию, спуститься по дереву каталогов к интересующей Вас подпапке и, нажав на нее, просмотреть содержащиеся в ней файлы. "+ Environment.NewLine + Environment.NewLine;
            textBox1.Text += "В настройках Вы можете изменить цвет прдставления файлов определенных групп (офисные, графические, исполняемые, архивы) и изменить шрифт в представлении файлов в списке. " + Environment.NewLine + Environment.NewLine;
            textBox1.Text += "На диаграмме будет показана зависимость числа файлов от длины их имени (длинным файл считается при длине имени более 15 символов, средним при длине от 10 до 15, и маленьким при длине менее 10 символов. "+Environment.NewLine + Environment.NewLine;
            textBox1.Text += "Изначально все файлы помечены галочкой, однако можно их снимать, тем самым изменяя показания на диаграмме. " + Environment.NewLine + Environment.NewLine;
            textBox1.Text += "При нажатии на кнопку \"Сохранить\" Вам будет предложен выбор места сохранения текстового файла с данными из списка. " + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            textBox1.Text += "\t\t        ПРИЯТНОГО ПОЛЬЗОВАНИЯ!";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
