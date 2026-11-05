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

namespace Otopark_Proje
{
    public partial class AboneKayitlari : Form
    {
        public AboneKayitlari()
        {
            InitializeComponent();
        }
                
        private void AboneKayitlari_Load(object sender, EventArgs e)
        {
            Form1.KayitListele(AboneKayitlari_DataGrid, "Aboneler");
        }

        private void AboneKayitlari_KayitSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow seciliSatir = AboneKayitlari_DataGrid.CurrentRow;
            if (seciliSatir == null)
            {
                MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int secilenKayitID = Convert.ToInt32(seciliSatir.Cells["AboneId"].Value);

            KayitSilOnayForm onayForm = new KayitSilOnayForm("Abone Kaydı", seciliSatir);
            if (onayForm.ShowDialog() == DialogResult.OK && onayForm.Onaylandi)
            {
                try
                {
                    Form1.BaglantiAc();
                    SqlCommand SilKomut = new SqlCommand("DELETE FROM Aboneler WHERE AboneId=@pID", Form1.Baglanti);
                    SilKomut.Parameters.AddWithValue("@pID", secilenKayitID);
                    SilKomut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.KayitListele(AboneKayitlari_DataGrid, "Aboneler");
                }
                catch (Exception Hata)
                {
                    MessageBox.Show("Kayıt silinirken hata oluştu:\n" + Hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (Form1.Baglanti.State == System.Data.ConnectionState.Open)
                        Form1.Baglanti.Close();
                }
            }
        }
    }
}
