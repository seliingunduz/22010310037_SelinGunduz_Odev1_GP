using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace gorselprogramlama;

public partial class Renkseçme : ContentPage
{
    public Renkseçme()
    {
        InitializeComponent();
    }

    // Rengin değiştiğini almak için 
    private void OnColorChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'lardan alınan renklerin değerleri için
        int red = (int)RedSlider.Value;
        int green = (int)GreenSlider.Value;
        int blue = (int)BlueSlider.Value;

        // Rengi oluşturup gönderdim 
        Color color = Color.FromRgb(red, green, blue);
        ColorPreview.Color = color;

        // Renk etiketi
        ColorCodeLabel.Text = $"#{red:X2}{green:X2}{blue:X2}";
    }

    // Kopyalama butonu
    private async void OnCopyColorCodeClicked(object sender, EventArgs e)
    {
        string colorCode = ColorCodeLabel.Text;

        // Kopyalama işlemi
        await Clipboard.SetTextAsync(colorCode);
        await DisplayAlert("Başarılı", "Renk kodu kopyalandı!", "Tamam");
    }

    // Random renk seçme butonu
    private void OnRandomColorClicked(object sender, EventArgs e)
    {
        // Rastgele değerler için
        Random random = new Random();
        int red = random.Next(0, 256);
        int green = random.Next(0, 256);
        int blue = random.Next(0, 256);

        // Rastgel renklerin değerleri için
        RedSlider.Value = red;
        GreenSlider.Value = green;
        BlueSlider.Value = blue;

        // Rengi oluşturdum gönderdim
        Color color = Color.FromRgb(red, green, blue);
        ColorPreview.Color = color;
        //renk etiketi
        ColorCodeLabel.Text = $"#{red:X2}{green:X2}{blue:X2}";
    }
}
