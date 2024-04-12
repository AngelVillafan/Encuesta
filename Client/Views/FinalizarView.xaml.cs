namespace Client.Views;

public partial class FinalizarView : ContentPage
{
	public FinalizarView()
	{
		InitializeComponent();
        this.BindingContext = App.encuestaViewModel;
    }
}