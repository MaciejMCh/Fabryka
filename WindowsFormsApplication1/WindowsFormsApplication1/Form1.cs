using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Fabryka
{

    

    public partial class Form1 : Form
    {

        Thread proces1Thread;
        Thread proces2Thread;
        Thread proces3Thread;
        Thread proces4Thread;

        Produkt proces1Produkt;
        Produkt proces2Produkt;
        Produkt proces3Produkt;
        Produkt proces4Produkt;

        List<Produkt> zamowienia1 = new List<Produkt>();
        List<Produkt> zamowienia2 = new List<Produkt>();
        List<Produkt> magazyn = new List<Produkt>();

        Barrier barrier = new Barrier(3);

        public Form1()
        {
            InitializeComponent();
            this.proces1Thread = new Thread(proces1);
            this.proces2Thread = new Thread(proces2);
            this.proces3Thread = new Thread(proces3);
            this.proces4Thread = new Thread(proces4);

            this.proces1Thread.Start();
            this.proces2Thread.Start();
            this.proces3Thread.Start();
            this.proces4Thread.Start();

        }

        private void proces1()
        {
            while (true)
            {
                if (zamowienia1.Count > 0 && this.proces1Produkt == null)
                {
                    Produkt produkt = this.zamowienia1[this.zamowienia1.Count - 1];
                    this.zamowienia1.Remove(produkt);
                    this.proces1Produkt = produkt;

                    for (int i = 0; i <= 100; i++)
                    {
                        produkt.proces1 = i;

                        Thread.Sleep(10);
                    }

                }

                Thread.Sleep(10);
            }
        }
        private void proces2()
        {
            while (true)
            {
                if (this.proces1Produkt != null && this.proces1Produkt.proces1 == 100 && this.proces2Produkt == null)
                {
                    this.proces2Produkt = this.proces1Produkt;
                    this.proces1Produkt = null;

                    for (int i = 0; i <= 100; i++)
                    {
                        this.proces2Produkt.proces2 = i;

                        Thread.Sleep(10);
                    }

                    this.barrier.SignalAndWait();
                }

                Thread.Sleep(10);
            }
        }
        private void proces3()
        {
            while (true)
            {
                if (this.zamowienia2.Count > 0 && this.proces3Produkt == null)
                {
                    Produkt produkt = this.zamowienia2[this.zamowienia2.Count - 1];
                    this.zamowienia2.Remove(produkt);
                    this.proces3Produkt = produkt;

                    for (int i = 0; i <= 100; i++)
                    {
                        produkt.proces3 = i;

                        Thread.Sleep(10);
                    }

                    this.barrier.SignalAndWait();
                }

                Thread.Sleep(10);
            }
        }
        private void proces4()
        {
            while (true)
            {
                this.barrier.SignalAndWait();

                Produkt produkt1 = this.proces2Produkt;
                Produkt produkt2 = this.proces3Produkt;
                this.proces4Produkt = produkt1;
                this.proces2Produkt = null;
                this.proces3Produkt = null;

                for (int i = 0; i <= 100; i++)
                {
                    produkt1.proces4 = i;
                    produkt2.proces4 = i;

                    Thread.Sleep(10);
                }

                this.magazyn.Add(produkt1);
                this.magazyn.Add(produkt2);

                proces4Produkt = null;

                Thread.Sleep(10);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.zamowienia1.Add(new Produkt());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = this.proces1Produkt == null ? 0 : this.proces1Produkt.proces1;
            this.progressBar2.Value = this.proces2Produkt == null ? 0 : this.proces2Produkt.proces2;
            this.progressBar3.Value = this.proces3Produkt == null ? 0 : this.proces3Produkt.proces3;
            this.progressBar4.Value = this.proces4Produkt == null ? 0 : this.proces4Produkt.proces4;

            this.label1.Text = "zamówienia: " + this.zamowienia1.Count.ToString();
            this.label2.Text = "zamówienia: " + this.zamowienia2.Count.ToString();

            this.label3.Text = "magazyn: " + this.magazyn.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.zamowienia2.Add(new Produkt());
        }




    }
}
