
using System;

namespace gorselprogramlama
{
    public partial class Kredi : ContentPage
    {
        public Kredi()
        {
            InitializeComponent();
        }

        // T�r se�imi yapt�rd�m
        private async void KrediT�r�Se�Butonu_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Kredi T�r� Se�in", "�ptal", null, "�htiya� Kredisi", "Konut Kredisi", "Ta��t Kredisi", "Ticari Kredi");

            if (!string.IsNullOrEmpty(result) && result != "�ptal")
            {
                KrediT�r�Se�Butonu.Text = result; // Se�ilen t�r� butonda kald�
            }
        }

        // Vade kayd�r�c� koydum �rnektekinden farkl� olsun diye
        private void LoanTermSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int selectedTerm = (int)e.NewValue; // De�eri ald�m
            TermLabel.Text = $"{selectedTerm} Ay"; 
        }

        // Hesapla butonu i�in 
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                // Kullan�c�dan al�nan de�erler
                double krediTutar� = double.Parse(LoanAmountEntry.Text);  
                double faizOran� = double.Parse(InterestRateEntry.Text) / 100;  
                int vadeS�resi = int.Parse(TermLabel.Text.Replace(" Ay", ""));  

                //kkdf ve bsmv'yi �rnekteki gibi ald�m
                double kdv = 0, bsmv = 0;

                if (KrediT�r�Se�Butonu.Text == "�htiya� Kredisi")
                {
                    kdv = 0.15; 
                    bsmv = 0.10;
                }
                else if (KrediT�r�Se�Butonu.Text == "Konut Kredisi")
                {
                    kdv = 0; 
                    bsmv = 0; 
                }
                else if (KrediT�r�Se�Butonu.Text == "Ta��t Kredisi")
                {
                    kdv = 0.15; 
                    bsmv = 0.05; 
                }
                else if (KrediT�r�Se�Butonu.Text == "Ticari Kredi")
                {
                    kdv = 0; 
                    bsmv = 0.05; 
                }

                // Hesaplamalar� yapt�m 
                double brutFaiz = ((faizOran� + (faizOran� * bsmv) + (faizOran� * kdv))/100);
                double taksit = ((Math.Pow(1 + brutFaiz, vadeS�resi) * brutFaiz) / (Math.Pow(1 + brutFaiz, vadeS�resi) - 1)) * krediTutar�;
                double toplam�deme = taksit * vadeS�resi;
                double toplamFaiz = toplam�deme - krediTutar�;

                // Ekranda g�stermek i�in 
                MonthlyPaymentLabel.Text = $"{taksit:F2} TL";
                TotalPaymentLabel.Text = $"{toplam�deme:F2} TL";
                TotalInterestLabel.Text = $"{toplamFaiz:F2} TL";
            }
            catch (Exception h)
            {
                // hatay� kontrol i�in
                DisplayAlert("Hata", "L�tfen t�m alanlar� do�ru �ekilde doldurun.", "Tamam");
            }
        }
    }
}
