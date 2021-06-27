using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }
        DbEntityUrunEntities dbEntityUrunEntities = new DbEntityUrunEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //  dataGridView1.DataSource = dbEntityUrunEntities.TBLURUN.ToList();

            dataGridView1.DataSource = (from x in dbEntityUrunEntities.TBLURUN
                                        select new
                                        {
                                            x.URUNID,
                                            x.URUNAD,
                                            x.MARKA,
                                            x.STOK,
                                            x.FIYAT,
                                            x.TBLKATEGORI.AD,
                                            x.DURUM

                                        }).ToList();
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TBLURUN t = new TBLURUN();
            t.URUNAD = TxtUrunAd.Text;
            t.MARKA = TxtMarka.Text;
            t.STOK = short.Parse(TxtStok.Text);
            t.KATEGORI = int.Parse(comboBox1.SelectedValue.ToString());
            t.FIYAT = decimal.Parse(TxtFiyat.Text);
            t.DURUM = true;
            dbEntityUrunEntities.TBLURUN.Add(t);
            dbEntityUrunEntities.SaveChanges();
            MessageBox.Show("Ürün eklendi");

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            var urun = dbEntityUrunEntities.TBLURUN.Find(x);
            dbEntityUrunEntities.TBLURUN.Remove(urun);
            dbEntityUrunEntities.SaveChanges();
            MessageBox.Show("Ürün silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            var urun = dbEntityUrunEntities.TBLURUN.Find(x);
            urun.URUNAD = TxtUrunAd.Text;
            urun.STOK = short.Parse(TxtStok.Text);
            urun.MARKA = TxtMarka.Text;
            dbEntityUrunEntities.SaveChanges();
            MessageBox.Show("Ürün güncellendi");
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            var kategoriler = (from x in dbEntityUrunEntities.TBLKATEGORI
                               select new
                               {
                                   x.ID,
                                   x.AD

                               }).ToList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "AD";
            comboBox1.DataSource = kategoriler;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TxtUrunAd.Text = comboBox1.SelectedValue.ToString();
        }
    }
}
