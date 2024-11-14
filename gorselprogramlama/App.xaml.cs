namespace gorselprogramlama
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent(); //app xaml tüm bileşenler burada toplanır

            MainPage = new AppShell(); //anasayfayı tanımladık
        }
    }
}
