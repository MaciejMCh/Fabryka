using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serial1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!serialPort1.IsOpen) {
                serialPort1.Open();
                textBox2.Text = "otwarty";
            } else {
                textBox2.Text = "zajety";
            }
        }

        public static string TekstOdbierany = "";
        public static string TekstWysylany = "";

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {

                TekstWysylany = textBox1.Text;
                serialPort1.WriteLine(TekstWysylany);


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

            serialPort1.Write(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
               // serialPort1.Open();

        } 

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }
        
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            TekstOdbierany = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(WyswietlTekst));
        }



        private void WyswietlTekst(object s, EventArgs e)
        {
            textBox1.AppendText(TekstOdbierany);
        }
    }
}

