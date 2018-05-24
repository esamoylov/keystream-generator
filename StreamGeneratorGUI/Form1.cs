using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace StreamGeneratorGUI
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        const string fileName = "generator.txt";
        string filePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in textBox1.Text)
            {
                if (!Char.IsDigit(c))
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Недопустимый символ!", "Ошибка");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in textBox2.Text)
            {
                if (!(Char.IsDigit(c) || Char.IsWhiteSpace(c)))
                {
                    textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Недопустимый символ!", "Ошибка");
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in textBox3.Text)
            {
                if (!(Char.IsDigit(c) || Char.IsWhiteSpace(c)))
                {
                    textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Недопустимый символ!", "Ошибка");
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in textBox4.Text)
            {
                if (!(Char.IsDigit(c) || Char.IsWhiteSpace(c)))
                {
                    textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Недопустимый символ!", "Ошибка");
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in richTextBox1.Text)
            {
                if (!(Char.IsDigit(c) || Char.IsWhiteSpace(c) || c == 'x' || c == '+'))
                {
                    richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1);
                    MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                    MessageBox.Show("Недопустимый символ!", "Ошибка");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
            }
            else
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
            }
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {
            
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (File.Exists(fileName))
                File.Delete(fileName);

            File.Create(fileName).Close();
            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            MessageBox.Show($"Файл {fileName} создан", "Успешно");
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(FormAbout frm = new FormAbout())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ofd.Title = "Выберите файл";
            ofd.Filter = "Текстовые файлы|*.txt";

            if (ofd.ShowDialog() != DialogResult.OK) return;
            else
                filePath = ofd.InitialDirectory + ofd.FileName;

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {            
            
            List<string> regsList = new List<string>();

            if (!Int64.TryParse(textBox1.Text, out long length))
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Необходимо ввести длину последовательности!", "Ошибка");
                return;
            }

            if (textBox2.Text == String.Empty)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Необходимо ввести функцию регистра сдвига!", "Ошибка");
                return;
            }

            regsList.Add(textBox2.Text);

            if (textBox3.Text != String.Empty)
                regsList.Add(textBox3.Text);
            if (textBox4.Text != String.Empty)
                regsList.Add(textBox4.Text);

            string[] lfsr = regsList.ToArray();

            if (richTextBox1.Text == String.Empty)
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Необходимо ввести булеву функцию!", "Ошибка");
                return;
            }
            string boolFunc = richTextBox1.Text;

            string path = String.Empty;

            if (filePath != string.Empty)
                path = filePath;
            if (File.Exists(fileName))
                path = fileName;
            if (String.IsNullOrEmpty(path))
            {
                MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
                MessageBox.Show("Hеобходимо выбрать файл!", "Ошибка");
                return;
            }

            BooleanFunction bf = new BooleanFunction(lfsr, boolFunc);

            bf.GenBits();

            if (radioButton1.Checked)
                bf.CombiningGenerator(length, path);
            else
                bf.FilferGenerator(length, path);

            MessageBoxHelper.PrepToCenterMessageBoxOnForm(this);
            MessageBox.Show("Последовательность сгенерирована", "Успешно");     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
        }
    }
}