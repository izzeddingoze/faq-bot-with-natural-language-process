using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using StringSimilarity;

namespace FAQBOT
{    public partial class Form1 : Form
    {        
        int veri_sayisi = 0;
        private string[] sorular;
        string[] cevaplar;
        public Form1()
        {

            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string soru = ask_box.Text;
            int mostsimindex = 0;
            double max_rate=0, rate,min_rate;

            SentenceSimilarity enbenzeribul = new SentenceSimilarity(soru, sorular);
            soru = enbenzeribul.LEMMATIZATION_SENTENCE(soru);        

            min_rate = 999;
            for (int i = 0; i < sorular.Length; i++)
            {                
                rate = enbenzeribul.JENSEN_SHANNON_RATE(soru, enbenzeribul.LEMMATIZATION_SENTENCE(sorular[i]));                
                if (rate < min_rate)
                {
                    min_rate = rate;
                    mostsimindex = i;
                }
            }
            answer_box.Text = cevaplar[mostsimindex];
            min_rate = 999;
       }
        private void button2_Click(object sender, EventArgs e) // temizle butonu
        {
            answer_box.Text = "";
            ask_box.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            OleDbConnection veri_baglantisi = new OleDbConnection();
            veri_baglantisi.ConnectionString = @"provider=microsoft.jet.Oledb.4.0; data Source=faq_db.mdb";
            OleDbCommand komut1= new OleDbCommand();
            komut1.Connection = veri_baglantisi;
            komut1.CommandType = CommandType.Text;
            komut1.CommandText = "SELECT *from Table1";
            OleDbDataReader oku;


            veri_baglantisi.Open(); 
            oku = komut1.ExecuteReader();
            while (oku.Read())
            {veri_sayisi++;}
            veri_baglantisi.Close();       

            sorular = new string[veri_sayisi];
            cevaplar = new string[veri_sayisi];

            int i = 0;
            veri_baglantisi.Open(); 
            oku = komut1.ExecuteReader();
            while (oku.Read())
            {
                sorular[i] = oku["Soru"].ToString();
                cevaplar[i]= oku["Cevap"].ToString();
                i++;
            }
            veri_baglantisi.Close(); 
        }
    }
}
