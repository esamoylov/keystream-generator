using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StreamGeneratorGUI
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            lblProdName.Text = string.Format("Название Продукта: {0}", Application.ProductName);
            lblProdVersion.Text = string.Format("Версия: {0}", Application.ProductVersion);
            lblCopyright.Text = "Copyright © Евгений Самойлов 2018";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
