using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace seminarski_is
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connString = (@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\db.mdb;");
        OleDbConnection conn;
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        string[] status = { "Definitivni", "Nacrt", "Objavljen", "Povucen", "Projekat"};

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = "";
            double din=-1, chf=-1, sumDin=0.0, sumChf=0.0;
            Text = "Standardi - Učitavanje...";
            textBox1.Text = "";
            textBox1.Invalidate();
            conn = new OleDbConnection(connString);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM standardi";
            label2.Text = "Ukupan broj standarda: \r\n " + ((int)cmd.ExecuteScalar()).ToString() + "\r\n";
            cmd.CommandText = "SELECT COUNT(*) FROM standardi WHERE status = '" + status[comboBox1.SelectedIndex] + "'";
            label2.Text += "\r\n" + status[comboBox1.SelectedIndex] + ":\r\n " + ((int)cmd.ExecuteScalar()).ToString() + "\r\n";
            cmd.CommandText = "SELECT * FROM standardi WHERE status = '" + status[comboBox1.SelectedIndex] + "'";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                din = Convert.ToDouble(dr.GetValue(3));
                sumDin += din;
                if (din == 0)
                    chf = 0;
                else
                {
                    chf = ((double)(din / 114));
                    sumChf += chf;
                }
                tmp += "Naslov: " + dr.GetValue(2) + "\r\n";
                tmp += "Sifra: " + dr.GetValue(1) + "\r\n";
                tmp += "Cena: " + din.ToString() + " DIN, "+chf.ToString("0.##")+" CHF\r\n";
                tmp += "Status: " + dr.GetValue(4) + "\r\n";
                tmp += "Datum promena: " + dr.GetValue(5) + "\r\n";
                tmp += "Godina nastanka: " + dr.GetValue(6) + "\r\n";
                tmp += "-------------------------------------------------------------------------------------------------------------------\r\n";
            }
            textBox1.Text = tmp;
            label5.Text = sumDin.ToString("0.##");
            label6.Text = sumChf.ToString("0.##");
            dr.Close();
            conn.Close();
            Text = "Standardi";
            textBox1.Focus();
            textBox1.SelectionLength = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            bg.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Parent = bg;
            pictureBox1.Parent = bg;
            pictureBox2.Parent = bg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new author().ShowDialog();
        }
    }
}
