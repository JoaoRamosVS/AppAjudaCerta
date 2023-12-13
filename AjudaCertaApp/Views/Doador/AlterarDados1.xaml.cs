namespace AjudaCertaApp.Views.Doador;

public partial class AlterarDados1 : ContentPage
{
	public AlterarDados1()
	{
		InitializeComponent();
	}

    private void pfpj_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pfpj.SelectedIndex == 0)
        {
            etCpf.IsVisible = true;
            lblCpf.IsVisible = true;

            etCnpj.IsVisible = false;
            lblCnpj.IsVisible = false;

            lblNome.Text = "Nome completo:";

            lblDataNasc.IsVisible = true;
            dtpDataNasc.IsVisible = true;

            lblGenero.IsVisible = true;
            pckGenero.IsVisible = true;

        }
        else if (pfpj.SelectedIndex == 1)
        {
            etCpf.IsVisible = false;
            lblCpf.IsVisible = false;

            etCnpj.IsVisible = true;
            lblCnpj.IsVisible = true;

            lblNome.Text = "Nome da Empresa:";

            lblDataNasc.IsVisible = false;
            dtpDataNasc.IsVisible = false;

            lblGenero.IsVisible = false;
            pckGenero.IsVisible = false;
        }

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        entNome.IsEnabled = true;
        entUsuario.IsEnabled = true;
        entEmail.IsEnabled = true;
        entTelefone.IsEnabled = true;
        pkrPfpj.IsEnabled = true;       
        btnManter.IsVisible = false;
        btnAlterar.IsVisible = true;
    }
}