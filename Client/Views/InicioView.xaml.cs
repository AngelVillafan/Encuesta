namespace Client.Views;

public partial class InicioView : ContentPage
{
	public InicioView()
	{
		InitializeComponent();
        this.BindingContext = App.encuestaViewModel;
    }
}