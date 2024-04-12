using Client.Model;
using Client.ViewModel;

namespace Client.Views;

public partial class EncuestaView : ContentPage
{
	public EncuestaView()
	{
		InitializeComponent();
		this.BindingContext = App.encuestaViewModel;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        EncuestaViewModel vm = (EncuestaViewModel)this.BindingContext;
        Button button = sender as Button;
        object[] parametros = button.CommandParameter as object[];
        if (vm != null)
        {
            Respuesta respuesta = new Respuesta()
            {
                Id = (int)parametros[0],
                Valor = (int)parametros[1]
            };

            vm.ChangeValorCommand?.Execute(respuesta);
        }
    }
}