using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MyVocabulary
{
    public partial class Form3 : Form
    {
        //tworzenie mapy słowo - tłumaczenie
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //zmienna do losowania słów
        Random rand = new Random();

        /// <summary>
        /// klasa typu public Form3
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// metoa SetLabel
        /// </summary>
        public void SetLabelText()
        {
            //losowanie slowa i jego wyświetlenie
            List<string> keys = new List<string>(dictionary.Keys);
            int size = dictionary.Count;
            Random rand = new Random();
            label2.Text = keys[rand.Next(size)];
        }

        private void Form3_Load(object sender, EventArgs e)
        {
             DirectoryInfo dinfo = new DirectoryInfo(@"C:\MyVocabulary");
             FileInfo[] Files = dinfo.GetFiles("*.txt");
             comboBox1.Items.AddRange(Files);
        }

       /// <summary>
       /// wyświetlenie słów z tłumacznika
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //sprawdza czy jest wybrany tłumacznik
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Select vocabulary!");
            }
            else
            {
                //odczyta plik textowy i wyświetlenie go na textBox2
                string QW = @"C:\MyVocabulary\";
                using (var sr = new StreamReader(QW + comboBox1.Text))
                {
                    var str = sr.ReadToEnd();
                    textBox2.Text = str.ToString();
                    textBox2.Visible = true;
                    button3.Visible = false;
                    textBox1.Visible = false;
                    label2.Visible = false;
                }
            }
        }
        /// <summary>
        /// Uzupewnienie warunków działalności
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //sprawdza czy jest wybrany tłumacznik
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Select vocabulary!");
            }
            else
            {
                dictionary.Clear();
                button3.Visible = true;
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Visible = false;

                StreamReader m_streamReader = new StreamReader(@"C:\MyVocabulary\" + comboBox1.Text);

                // Czytanie pliku za pomocą klasy streamreader
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                string strLine = m_streamReader.ReadLine();
                while (strLine != null)
                {
                    
                    string[] tokens = strLine.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    // rozbicie wierszy na słowo oraz jego tłumaczenie i zapisanie do macierzy
                    for (int i = 0; i < tokens.Length; i += 2)
                    {
                        string name = tokens[i];
                        string freq = tokens[i + 1];

                        // Wypełnić wartości w posortowanej słowniku
                        if (dictionary.ContainsKey(name))
                        {
                            dictionary[name] += freq;
                        }
                        else
                        {
                            dictionary.Add(name, freq);
                        }
                    }
                    strLine = m_streamReader.ReadLine();
                }

                SetLabelText();

             
                m_streamReader.Close();
            }
        }
        /// <summary>
        /// sprawdza poprawność wpisanego słowa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == dictionary[label2.Text])
            {
                label3.Visible = true;
                label3.Text = "GOOD! ^_^";
                label3.ForeColor = Color.Green;
            }
            else
            {
                label3.Visible = true;
                label3.Text = "NOT GOOD! :(";
                label3.ForeColor = Color.Red;
            }

            SetLabelText();
            textBox1.Text = null;
        }
        private void comboBox1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
        }
    }
}
