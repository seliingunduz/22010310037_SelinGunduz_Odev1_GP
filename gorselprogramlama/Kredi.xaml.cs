
using System;

namespace gorselprogramlama
{
    public partial class Kredi : ContentPage
    {
        public Kredi()
        {
            InitializeComponent();
        }

        // Tür seçimi yaptýrdým
        private async void KrediTürüSeçButonu_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Kredi Türü Seçin", "Ýptal", null, "Ýhtiyaç Kredisi", "Konut Kredisi", "Taþýt Kredisi", "Ticari Kredi");

            if (!string.IsNullOrEmpty(result) && result != "Ýptal")
            {
                KrediTürüSeçButonu.Text = result; // Seçilen türü butonda kaldý
            }
        }

        // Vade kaydýrýcý koydum örnektekinden farklý olsun diye
        private void LoanTermSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int selectedTerm = (int)e.NewValue; // Deðeri aldým
            TermLabel.Text = $"{selectedTerm} Ay"; 
        }

        // Hesapla butonu için 
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                // Kullanýcýdan alýnan deðerler
                double krediTutarý = double.Parse(LoanAmountEntry.Text);  
                double faizOraný = double.Parse(InterestRateEntry.Text) / 100;  
                int vadeSüresi = int.Parse(TermLabel.Text.Replace(" Ay", ""));  

                //kkdf ve bsmv'yi örnekteki gibi aldým
                double kdv = 0, bsmv = 0;

                if (KrediTürüSeçButonu.Text == "Ýhtiyaç Kredisi")
                {
                    kdv = 0.15; 
                    bsmv = 0.10;
                }
                else if (KrediTürüSeçButonu.Text == "Konut Kredisi")
                {
                    kdv = 0; 
                    bsmv = 0; 
                }
                else if (KrediTürüSeçButonu.Text == "Taþýt Kredisi")
                {
                    kdv = 0.15; 
                    bsmv = 0.05; 
                }
                else if (KrediTürüSeçButonu.Text == "Ticari Kredi")
                {
                    kdv = 0; 
                    bsmv = 0.05; 
                }

                // Hesaplamalarý yaptým 
                double brutFaiz = ((faizOraný + (faizOraný * bsmv) + (faizOraný * kdv))/100);
                double taksit = ((Math.Pow(1 + brutFaiz, vadeSüresi) * brutFaiz) / (Math.Pow(1 + brutFaiz, vadeSüresi) - 1)) * krediTutarý;
                double toplamÖdeme = taksit * vadeSüresi;
                double toplamFaiz = toplamÖdeme - krediTutarý;

                // Ekranda göstermek için 
                MonthlyPaymentLabel.Text = $"{taksit:F2} TL";
                TotalPaymentLabel.Text = $"{toplamÖdeme:F2} TL";
                TotalInterestLabel.Text = $"{toplamFaiz:F2} TL";
            }
            catch (Exception h)
            {
                // hatayý kontrol için
                DisplayAlert("Hata", "Lütfen tüm alanlarý doðru þekilde doldurun.", "Tamam");
            }
        }
    }
}
