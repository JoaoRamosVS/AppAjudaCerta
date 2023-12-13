using Syncfusion.Maui.Picker;

namespace AjudaCertaApp.Views.Beneficiario;

public partial class AgendarRetirada : ContentPage
{
	public AgendarRetirada()
	{
		InitializeComponent();
        SfDateTimePicker picker = new SfDateTimePicker();
    }

    private void pickerButton_Clicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen = true;
    }

    private void Picker_OkButtonClicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen = false;
    }

    private void Picker_CancelButtonClicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen = false;
        this.Picker.SelectedDate = DateTime.Now;
    }
}