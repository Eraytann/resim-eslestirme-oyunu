using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] dizi = new int[12];
        PictureBox[] resim = new PictureBox[12];
        int icerik,icerik1=-1,icerik2=-1;
        int tiklananresim1, tiklananresim2;
        int ars = 0;
        int sure = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            sayiuret();
            pbox_olustur();
            pbox_bosyukle();
            timer1.Interval = 1000;
            timer1.Start();
        }
        
        private void sayiuret()
        {
            Random r = new Random();
            int gecici;
            for (int i = 0; i <= 11; i++)  // 12 elemanlı dizi içerisinde 0'dan 11'e kadar sayıları atıyor.
            {
               dizi[i] = -1;
                do
                {
                    gecici = r.Next(0, 12);
                } while (Array.IndexOf(dizi,gecici)!=-1);
                dizi[i] = gecici;
            }
            for (int i = 0; i <=11; i++)
            {
                if (dizi[i]>5)
                {
                    dizi[i] = dizi[i] % 6;
                }
            }
           
            
        }
        private void pbox_olustur()
        {
            int x =0;
            int y = 30;
            for (int i = 0; i < 12; i++)
            {
                if (i > 1 && i % 3 == 0) // her 3 pb da 1 alt satıra inme 
                {
                    y += 50; // pb ı alt satıra indirme
                    x = 0;  // pb ı en sağa aldık
                }
                x += 50;    // pb ı sağa kaydırdık
                resim[i] = new PictureBox();
                resim[i].Location = new Point(x, y);     // pb ı form üzerinde konumlandırma
                resim[i].Size = new Size(40, 40);       //  pb ın boyutu ayarlama
                resim[i].Click += new EventHandler(tikla);  // pb a click özelliği kazandırma
                resim[i].Tag = i;                           // hangi pb a tıklanıldığını öğrenmek için
                this.Controls.Add(resim[i]);                // pb a form üzerine ekleme
                resim[i].BorderStyle = BorderStyle.FixedSingle; // pb a çerceve ekleme
            }
        }
        private void pbox_bosyukle()    // dinamik olarak oluşturulan pb dizisine boş resimler yükleme
        {
            for (int i = 0; i < 12; i++)
            {
                resim[i].Image = ımageList1.Images[6];
            }
        }
        private void tikla(object sender, EventArgs y) // pb nesnesinin olayı
        {
            icerik =dizi[Convert.ToInt16(((PictureBox)sender).Tag)];
            if (icerik1==-1)   // ilk tıklanan resmi aç
            {
                icerik1 = icerik;
                tiklananresim1=Convert.ToInt16(((PictureBox)sender).Tag);
                resim[tiklananresim1].Image=ımageList1.Images[icerik1];
                return;
            }
            if (icerik1!=-1 && icerik2==-1) // 2 . tıklanan resmi aç
            {
                tiklananresim2=Convert.ToInt16(((PictureBox)sender).Tag);
                if (tiklananresim1!=tiklananresim2)
                {
                    icerik2 = icerik;
                    resim[tiklananresim2].Image = ımageList1.Images[icerik2];
                    this.Refresh();
                }
            }
            if (icerik1!=-1 && icerik2!=-1)
            {
                if (icerik1==icerik2)
                {
                    //MessageBox.Show("BİLDİNİZ");
                    ars++;
                    icerik1 = -1;
                    icerik2 = -1;
                    resim[tiklananresim1].Visible = false;
                    resim[tiklananresim2].Visible = false;
                    if (ars == 6)
                    {
                        timer1.Stop();
                        MessageBox.Show("SÜRE:"+sure.ToString(),"OYUN BİTTİ");
                        this.Close();
                    }
                }
                else
                {
                    //MessageBox.Show("BİLEMEDİNİZ");
                    icerik1 = -1;
                    icerik2 = -1;
                    System.Threading.Thread.Sleep(500);
                    resim[tiklananresim1].Image = ımageList1.Images[6];
                    resim[tiklananresim2].Image = ımageList1.Images[6];
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure++;
            label1.Text = sure.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sure += 3;
            for (int i = 0; i < 12; i++)
            {
                resim[i].Image = ımageList1.Images[dizi[i]];
            }
            this.Refresh();
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i < 12; i++)
            {
                resim[i].Image = ımageList1.Images[6];
            }
            button1.Enabled = false;
        }

    }
}
