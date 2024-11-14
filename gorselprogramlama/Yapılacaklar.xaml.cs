using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace gorselprogramlama
{
    public partial class Yap�lacaklar : ContentPage
    {
        ObservableCollection<Gorev> Gorevler = new ObservableCollection<Gorev>();

        public Yap�lacaklar()
        {
            InitializeComponent();
            GorevlerCollectionView.ItemsSource = Gorevler;

            // Tarih ve saat g�ncel kals�n diye g�ncellik eklemek i�in saat bilgiside ekledim
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TarihLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                return true; 
            });
        }

        // G�rev ekleme kutusu
        private void GorevEkleClicked(object sender, EventArgs e)
        {
            GorevEkleKutu.IsVisible = true;
        }

        // G�revi kaydetme
        private void GorevKaydet(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(YeniGorevEntry.Text)) // Bo� de�ilse
            {
                Gorevler.Add(new Gorev { GorevAd� = YeniGorevEntry.Text, Tamamlandi = false });
                YeniGorevEntry.Text = string.Empty;  // Textbox'� temizle
                GorevEkleKutu.IsVisible = false;  // Kutu tekrar gizlensin
            }
        }

        // G�revi silme
        private void GorevSil(object sender, EventArgs e)
        {
            var gorev = (Gorev)((Button)sender).CommandParameter;
            Gorevler.Remove(gorev);
        }

        // G�revi d�zenleme
        private async void GorevDuzenle(object sender, EventArgs e)
        {
            var gorev = (Gorev)((Button)sender).CommandParameter;
            var yeniGorevAd� = await DisplayPromptAsync("G�rev D�zenle", "G�revi D�zenle",
                                                        initialValue: gorev.GorevAd�);

            if (!string.IsNullOrWhiteSpace(yeniGorevAd�))
            {
                gorev.GorevAd� = yeniGorevAd�; 
            }
        }

        // G�rev s�nf�
        public class Gorev : INotifyPropertyChanged
        {
            private string _gorevAd�;
            public string GorevAd�
            {
                get => _gorevAd�;
                set
                {
                    if (_gorevAd� != value)
                    {
                        _gorevAd� = value;
                        OnPropertyChanged(nameof(GorevAd�));
                    }
                }
            }

            public bool Tamamlandi { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
