using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyVocabulary
{
    public partial class MyVocabulary : Form
    {
        public MyVocabulary()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2 CreateNewVocabulary = new form2();
            CreateNewVocabulary.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 OpenVocabulary = new Form3();
            OpenVocabulary.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You must write right translite. London is the capital of GB ^_^");
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My name is Roman ^_^");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
