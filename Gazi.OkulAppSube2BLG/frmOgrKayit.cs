﻿using OkulApp.BLL;
using OkulApp.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazi.OkulAppSube2BLG
{
    public partial class frmOgrKayit : Form
    {
        public int Ogrenciid { get; set; }

        public void TextEmpty(bool kontrol)
        {
            if (kontrol)
            {
                txtAd.Text = string.Empty;
                txtSoyad.Text = string.Empty;
                txtNumara.Text = string.Empty;
                btnSil.Visible = false;
                btnGuncelle.Visible = false;
                MessageBox.Show("İşlem başarılı");
            }
            else
            {
                MessageBox.Show("İşlem başarısız");
            }
        }

       
        public frmOgrKayit()
        {
            InitializeComponent();
        }


        //Dispose
        //Garbage Collector
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                var obl = new OgrenciBL();
                bool sonuc = obl.OgrenciEkle(new Ogrenci { Ad = txtAd.Text.Trim(), Soyad = txtSoyad.Text.Trim(), Numara = txtNumara.Text.Trim() });
                TextEmpty(sonuc);
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Bu numara daha önce kayıtlı");
                        break;
                    default:
                        MessageBox.Show("Veritabanı Hatası!");
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu!!");
            }
        


    }



    private void btnBul_Click(object sender, EventArgs e)
        {
            var frm = new frmOgrBul(this);
            frm.Show();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var obl = new OgrenciBL();
            TextEmpty(obl.OgrenciSil(Ogrenciid));
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            var obl = new OgrenciBL();
            TextEmpty(obl.OgrenciGuncelle(new Ogrenci { Ad = txtAd.Text.Trim(), Soyad = txtSoyad.Text.Trim(), Numara = txtNumara.Text.Trim(), Ogrenciid = Ogrenciid }));
            
        }
    }

    //interface ITransfer
    //{
    //    int EFT(string aliciiban, string gondereniban, double tutar);
    //    int Havale(string aliciiban, string gondereniban, double tutar, DateTime tarih);
    //}

    //class TransferIslemleri : ITransfer
    //{
    //    public int EFT(string aliciiban, string gondereniban, double tutar)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Havale(string aliciiban, string gondereniban, double tutar, DateTime tarih)
    //    {
    //        throw new NotImplementedException();
    //    }
   // }
}

//n Katmanlı Mimari

//Öğrenci bulunma durumuna göre Sil ve Güncelle Butonları Aktifliği   // Kolaydı 
//Textbox'ların text'lerinin temizlenmesi  //  Zorlanmadım
//Öğrenci bulunduğunda bul formunun kapanması // Eforsuz    
//Try-Catch'ler Katmanlar arası exception yönetimi // 
//Dispose Pattern - IDisposeble Interface
//Singleton Design Pattern
