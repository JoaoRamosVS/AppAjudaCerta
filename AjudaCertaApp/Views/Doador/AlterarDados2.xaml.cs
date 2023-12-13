namespace AjudaCertaApp.Views.Doador;

public partial class AlterarDados2 : ContentPage
{
	public AlterarDados2()
	{
		InitializeComponent();
	}

    private void btnAlterarDados_Clicked(object sender, EventArgs e)
    {
        entCep.IsEnabled = true;
        entComplemento.IsEnabled = true;
        btnConsultar.IsEnabled = true;
        entNumero.IsEnabled = true;
        btnManter.IsVisible = false;
        btnAlterar.IsVisible = true;
    }
}