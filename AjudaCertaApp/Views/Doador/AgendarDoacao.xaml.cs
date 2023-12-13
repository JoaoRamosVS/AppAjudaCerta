using AjudaCertaApp.Models;
using AjudaCertaApp.ViewModels.Doacao;
using Syncfusion.Maui.Picker;

namespace AjudaCertaApp.Views.Doador;

public partial class AgendarDoacao : ContentPage
{
    DoacaoViewModel doacaoViewModel;
	public AgendarDoacao(ItemDoacaoDoado aDoar)
	{
		InitializeComponent();
        doacaoViewModel = new DoacaoViewModel(aDoar);
        BindingContext = doacaoViewModel;
        SfDateTimePicker picker = new SfDateTimePicker();
    }

    

    private void pickerButton_Clicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen = true;
    }

    private void Picker_OkButtonClicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen = false;
        doacaoViewModel.DataAgenda = this.Picker.SelectedDate;
    }

    private void Picker_CancelButtonClicked(object sender, EventArgs e)
    {
        this.Picker.IsOpen =false;
        this.Picker.SelectedDate = DateTime.Now;
    }
}