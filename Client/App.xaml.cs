using Client.ViewModel;

namespace Client
{
    public partial class App : Application
    {
        public static EncuestaViewModel encuestaViewModel=new EncuestaViewModel();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}