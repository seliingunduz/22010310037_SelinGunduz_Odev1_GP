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
        // Kilo ve boyu aldým
        if (double.TryParse(KiloGirdisi.Text, out double kilo) && double.TryParse(BoyGirdisi.Text, out double boy))
        {
            //cm'den m'ye çevirdim
            boy = boy / 100;

            // VKÝ hesapla
            double vki = kilo / (boy * boy);
            VkiLabel.Text = vki.ToString("F2");  //sayýsal deðeri metine dönüþtürdüm

            // VKÝ kategorisini belirledim
            string vkiKategorisi = VkiKategorisiniGetir(vki);

            // Kategorisini ekrana yazdýrdým
            KategoriLabel.Text = $"Kategoriniz: {vkiKategorisi}";
        }
        else
        {   //giriþ yapýlmadýysa
            VkiLabel.Text = "Hatalý Giriþ!";
            KategoriLabel.Text = "Lütfen geçerli bir kilo ve boy giriniz.";
        }
    }

    private string VkiKategorisiniGetir(double vki)
    {   //if ile deðer aralýðýna göre kategoriyi belirledim
        if (vki < 16)
            return "Ýleri düzeyde zayýf";
        else if (vki >= 16 && vki < 16.99)
            return "Orta düzeyde zayýf";
        else if (vki >= 17 && vki < 18.49)
            return "Hafif düzeyde zayýf";
        else if (vki >= 18.50 && vki < 24.99)
            return "Normal kilolu";
        else if (vki >= 25 && vki < 29.99)
            return "Hafif þiþman, fazla kilolu";
        else if (vki >= 30 && vki < 34.99)
            return "1. derece obez";
        else if (vki >= 35 && vki < 39.99)
            return "2. derece obez";
        else  return "3. derece obez";
    }
}
