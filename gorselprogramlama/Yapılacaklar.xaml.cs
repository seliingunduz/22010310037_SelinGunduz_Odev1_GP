using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace gorselprogramlama
{
    public partial class Yapýlacaklar : ContentPage
    {
        ObservableCollection<Gorev> Gorevler = new ObservableCollection<Gorev>();

        public Yapýlacaklar()
        {
            InitializeComponent();
            GorevlerCollectionView.ItemsSource = Gorevler;

            // Tarih ve saat güncel kalsýn diye güncellik eklemek için saat bilgiside ekledim
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TarihLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                return true; 
            });
        }

        // Görev ekleme kutusu
        private void GorevEkleClicked(object sender, EventArgs e)
        {
            GorevEkleKutu.IsVisible = true;
        }

        // Görevi kaydetme
        private void GorevKaydet(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(YeniGorevEntry.Text)) // Boþ deðilse
            {
                Gorevler.Add(new Gorev { GorevAdý = YeniGorevEntry.Text, Tamamlandi = false });
                YeniGorevEntry.Text = string.Empty;  // Textbox'ý temizle
                GorevEkleKutu.IsVisible = false;  // Kutu tekrar gizlensin
            }
        }

        // Görevi silme
        private void GorevSil(object sender, EventArgs e)
        {
            var gorev = (Gorev)((Button)sender).CommandParameter;
            Gorevler.Remove(gorev);
        }

        // Görevi düzenleme
        private async void GorevDuzenle(object sender, EventArgs e)
        {
            var gorev = (Gorev)((Button)sender).CommandParameter;
            var yeniGorevAdý = await DisplayPromptAsync("Görev Düzenle", "Görevi Düzenle",
                                                        initialValue: gorev.GorevAdý);

            if (!string.IsNullOrWhiteSpace(yeniGorevAdý))
            {
                gorev.GorevAdý = yeniGorevAdý; 
            }
        }

        // Görev sýnfý
        public class Gorev : INotifyPropertyChanged
        {
            private string _gorevAdý;
            public string GorevAdý
            {
                get => _gorevAdý;
                set
                {
                    if (_gorevAdý != value)
                    {
                        _gorevAdý = value;
                        OnPropertyChanged(nameof(GorevAdý));
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
