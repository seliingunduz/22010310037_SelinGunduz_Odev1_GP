namespace gorselprogramlama
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigateToBMI(object sender, EventArgs e) //butona tıklanınca vki sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new BMI());
        }

        private async void OnNavigateToKrediHesaplama(object sender, EventArgs e) //butona tıklanınca kredi sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Kredi());
        }

        private async void OnNavigateToRenkSecme(object sender, EventArgs e) //butona tıklanınca renk seçm sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Renkseçme());
        }

        private async void OnNavigateToTodoList(object sender, EventArgs e) //butona tıklanınca yapılacaklar sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Yapılacaklar());
        }
    }
}
