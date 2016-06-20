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

namespace MyVocabulary
{
    public partial class form2 : Form
    {
        //tworzenie tłumacznika
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        List<string> keys; // lista słowa
        List<string> values;//lista tłumaczenia
        bool boolean; //zmienna która odpowiada za sprawdzanie warunków
        bool selectVocabularyBoolean;//zmienna która odpowiada za sprawdzanie warunków

        /// <summary>
        /// klassa  form2
        /// </summary>
        public form2()
        {
            InitializeComponent();
        }
        List<string> c = new List<string>();
        List<string> d = new List<string>();

        /// <summary>
        /// klassa SetLabelText
        /// </summary>
        public void SetLabelText()
        {
            //wprowadzenie słów oraz tłumaczeń do textBox4
            textBox4.Text = null;//usuwanie danych z textBox4
            if (keys != null || values != null)//sprawdzanie czy są dane w tłumaczniku
            {
                keys.Clear();
                values.Clear();
            }
            //wyświetla texty do textBox4 na podstawie ilośi słów
            int size = dictionary.Count;
            for (int i = 0; i < size; i++)
            {
                keys = new List<string>(dictionary.Keys);
                values = new List<string>(dictionary.Values);
                textBox4.AppendText(keys[i] + " - " + values[i] + "\n");
            }
        }
              /// <summary>
              /// Uzupewnienie poprawnego urzywania programu userem
              /// </summary>
              /// <param name="sender"></param>
              /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
           //sprawdza czy urzytkownik chce stworzyć nowy tłumacznik czy wprowadzić nowe dane do tłumacznika
            boolean = true;
            if (checkBox1.Checked == true)
            {
                //sprawdzamy czy wszyscy pola są uzupewnione
                if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Fill in all data fields!");
                    boolean = false;
                }
            }
            else
            {
                //sprawdz czy jest wybrany(aktywny)tłumacznik
                if (selectVocabularyBoolean == false)
                {
                    MessageBox.Show("Select vocabulary!");
                }
                else
                {
                    //sprawdzamy czy wszyscy pola są uzupewnione
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Fill in all data fields!");
                        boolean = false;
                    }
                }
            }
            //dodawanie danych do tłumacznika
            if (boolean == true)
            { 
            c.Add(textBox2.Text);
            d.Add(textBox3.Text);
            textBox4.AppendText(textBox2.Text + " - " + textBox3.Text + "\n");
            textBox2.Text = null;
            textBox3.Text = null;
        }


        }
        /// <summary>
        /// zapisanie danych do tłumacznika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
                        if (checkBox1.Checked == true)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Enter the name of vocabulary!");
                }
            }
            else
            {
                if (selectVocabularyBoolean == false)
                {
                    MessageBox.Show("Enter the name of vocabulary!");
                }
            }
            if (c.Count == 0)
            {
                MessageBox.Show("Add some words");
            }
            else
            {
                //ścieżka do zapisania tłumacznika na dysk
                string subPath = @"C:\MyVocabulary\";

                bool exists = System.IO.Directory.Exists(subPath);
                //jeżeli takiej ścieżki nie istnieje to program tworzy folder
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);
                StreamWriter textFile;
                //dodaje nowy plik do nowego tłumacznika
                if (checkBox1.Checked == true)
                {
                    textFile = new StreamWriter(@"C:\MyVocabulary\" + textBox1.Text + ".txt");
                }
                //dodaje nowy plik do istniejącego tłumacznika
                else
                {
                    textFile = new StreamWriter(@"C:\MyVocabulary\" + comboBox1.Text);
                }
                //zapisanie słowa+tłumaczenie do nowej wierzszy 
                for (int f = 0; f < c.Count; f++)
                {
                    textFile.WriteLine(c[f] + "-" + d[f]);
                }
                textFile.Close();
                MessageBox.Show("Save complete!");
            }
        }
        /// <summary>
        /// wygłąd zewnętrzny formy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        { 
            if (checkBox1.Checked == true)
            {
                textBox1.Visible = true;
                comboBox1.Visible = false;
                button3.Visible = false;
                comboBox1.Items.Clear();
                dictionary.Clear();
                c.Clear();
                d.Clear();
                textBox4.Text = null;
            }
            else
            {
                textBox4.Text = null;
                c.Clear();
                d.Clear();
                textBox1.Visible = false;
                comboBox1.Visible = true;
                button3.Visible = true;
                DirectoryInfo dinfo = new DirectoryInfo(@"C:\MyVocabulary");
                FileInfo[] Files = dinfo.GetFiles("*.txt");
                comboBox1.Items.AddRange(Files);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectVocabularyBoolean = true;
            c.Clear();
            d.Clear();
            //jeżęli nie wybrano tłumacznik z listy program wyświetla comunicat o tym
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Select vocabulary!");
            }
            else
            {
                dictionary.Clear();
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
                        c.Add(tokens[i]);

                        string freq = tokens[i + 1];
                        d.Add(tokens[i + 1]);

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
               //zamykanie streamReader
                m_streamReader.Close();
                SetLabelText();
            }
        }
        /// <summary>
        /// pobieranie listy tłumaczników z folderu MyVocabulary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form2_Load(object sender, EventArgs e)
        {
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\MyVocabulary");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            comboBox1.Items.AddRange(Files);
        }
        /// <summary>
        /// wybiera tłumacznik z listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectVocabularyBoolean = false;
        }

    }
}
