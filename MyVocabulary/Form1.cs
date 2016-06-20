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
        /// <summary>
        /// MyVocabulary
        /// </summary>
        public MyVocabulary()
        {
            InitializeComponent();
        }
        /// <summary>
        /// tworzymy nowy tłumacznik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2 CreateNewVocabulary = new form2();
            CreateNewVocabulary.ShowDialog();
        }
        /// <summary>
        /// odtwieramy już stworzony tłumacznik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 OpenVocabulary = new Form3();
            OpenVocabulary.ShowDialog();
        }
        /// <summary>
        /// zamykamy program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //informacja o korzystaniu programu
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here you can add or modiffy new dictionaries, which must help you with your lerning foreign languages.The seconf bennefit is testing your knowledge.");
        }
        /// <summary>
        /// informacja o developerze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program was created by Roman Kolisnyk ^_^");
        }
    }
}
