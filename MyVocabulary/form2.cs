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
        private MyVocabulary a;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        List<string> keys;
        List<string> values;
        bool boolean;
        bool selectVocabularyBoolean;


        public form2()
        {
            InitializeComponent();
        }

        List<string> c = new List<string>();
        List<string> d = new List<string>();

        public void SetLabelText()
        {
            textBox4.Text = null;
            if (keys != null || values != null)
            {
                keys.Clear();
                values.Clear();
            }

            int size = dictionary.Count;
            for (int i = 0; i < size; i++)
            {
                keys = new List<string>(dictionary.Keys);
                values = new List<string>(dictionary.Values);
                textBox4.AppendText(keys[i] + " - " + values[i] + "\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boolean = true;
            if (checkBox1.Checked == true)
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Fill in all data fields!");
                    boolean = false;
                }
            }
            else
            {
                if (selectVocabularyBoolean == false)
                {
                    MessageBox.Show("Select vocabulary!");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Fill in all data fields!");
                        boolean = false;
                    }
                }
                //////////////////////////////////////////////////////
            }
            if (boolean == true)
            { 
            c.Add(textBox2.Text);
            d.Add(textBox3.Text);
            textBox4.AppendText(textBox2.Text + " - " + textBox3.Text + "\n");
            textBox2.Text = null;
            textBox3.Text = null;
        }


        }

        //public object MyClass { get; private set; }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Enter the name of vocabilary!");
                }
            }
            else
            {
                //////////////////////////////////////////////

                if (selectVocabularyBoolean == false)
                {
                    MessageBox.Show("Enter the name of vocabilary!");
                }
            }
            if (c.Count == 0)
            {
                MessageBox.Show("Add some words");
            }
            else
            {
                string subPath = @"C:\MyVocabulary\"; // your code goes here

                bool exists = System.IO.Directory.Exists(subPath);

                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);
                StreamWriter textFile;
                if (checkBox1.Checked == true)
                {
                    textFile = new StreamWriter(@"C:\MyVocabulary\" + textBox1.Text + ".txt");
                }
                else
                {
                    textFile = new StreamWriter(@"C:\MyVocabulary\" + comboBox1.Text);
                }

                for (int f = 0; f < c.Count; f++)
                {
                    textFile.WriteLine(c[f] + "-" + d[f]);
                }
                textFile.Close();
                MessageBox.Show("Save complete!");
            }
        }

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

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Select vocabulary!");
            }
            else
            {
                dictionary.Clear();
                StreamReader m_streamReader = new StreamReader(@"C:\MyVocabulary\" + comboBox1.Text);

                // Read to the file using StreamReader  class
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                string strLine = m_streamReader.ReadLine();
                while (strLine != null)
                {
                    string[] tokens = strLine.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    // Walk through each item
                    for (int i = 0; i < tokens.Length; i += 2)
                    {
                        string name = tokens[i];
                        c.Add(tokens[i]);

                        string freq = tokens[i + 1];
                        d.Add(tokens[i + 1]);

                        // Fill the value in the sorted dictionary
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
                // Close the stream
                m_streamReader.Close();
                SetLabelText();
            }
        }

        private void form2_Load(object sender, EventArgs e)
        {
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\MyVocabulary");
            FileInfo[] Files = dinfo.GetFiles("*.txt");
            comboBox1.Items.AddRange(Files);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectVocabularyBoolean = false;
        }

    }
}
