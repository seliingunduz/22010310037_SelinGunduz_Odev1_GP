using Microsoft.Maui;

namespace gorselprogramlama;

public partial class BMI : ContentPage
{
    public BMI()
    {
        InitializeComponent();
    }

    private void VkiHesapla(object sender, EventArgs e)
    {
        // Kilo ve boyu ald�m
        if (double.TryParse(KiloGirdisi.Text, out double kilo) && double.TryParse(BoyGirdisi.Text, out double boy))
        {
            //cm'den m'ye �evirdim
            boy = boy / 100;

            // VK� hesapla
            double vki = kilo / (boy * boy);
            VkiLabel.Text = vki.ToString("F2");  //say�sal de�eri metine d�n��t�rd�m

            // VK� kategorisini belirledim
            string vkiKategorisi = VkiKategorisiniGetir(vki);

            // Kategorisini ekrana yazd�rd�m
            KategoriLabel.Text = $"Kategoriniz: {vkiKategorisi}";
        }
        else
        {   //giri� yap�lmad�ysa
            VkiLabel.Text = "Hatal� Giri�!";
            KategoriLabel.Text = "L�tfen ge�erli bir kilo ve boy giriniz.";
        }
    }

    private string VkiKategorisiniGetir(double vki)
    {   //if ile de�er aral���na g�re kategoriyi belirledim
        if (vki < 16)
            return "�leri d�zeyde zay�f";
        else if (vki >= 16 && vki < 16.99)
            return "Orta d�zeyde zay�f";
        else if (vki >= 17 && vki < 18.49)
            return "Hafif d�zeyde zay�f";
        else if (vki >= 18.50 && vki < 24.99)
            return "Normal kilolu";
        else if (vki >= 25 && vki < 29.99)
            return "Hafif �i�man, fazla kilolu";
        else if (vki >= 30 && vki < 34.99)
            return "1. derece obez";
        else if (vki >= 35 && vki < 39.99)
            return "2. derece obez";
        else  return "3. derece obez";
    }
}
