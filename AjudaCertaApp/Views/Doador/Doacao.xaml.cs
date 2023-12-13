using AjudaCertaApp.ViewModels.Doacao;

namespace AjudaCertaApp.Views.Doador;

public partial class Doacao : ContentPage
{
	DoacaoViewModel viewModel;
	public Doacao()
	{
		InitializeComponent();
		viewModel = new DoacaoViewModel();
		BindingContext = viewModel;
	}



    private void pckTipoDoacao_SelectedIndexChanged(object sender, EventArgs e)
    {
		if (pckTipoDoacao.SelectedIndex == 0) 
		{
			GridProduto.IsVisible = false;
			GridEletro.IsVisible = false;
			GridRoupa.IsVisible = false;
			GridMobilia.IsVisible = false;
			GridDinheiro.IsVisible = true;
            GridCestaBasica.IsVisible = false;
		}
		else if (pckTipoDoacao.SelectedIndex == 1)
		{
            GridProduto.IsVisible = true;
            GridEletro.IsVisible = false;
            GridRoupa.IsVisible = false;
            GridMobilia.IsVisible = false;
            GridDinheiro.IsVisible = false;
            GridCestaBasica.IsVisible = false;

        }
        else if (pckTipoDoacao.SelectedIndex == 2)
		{
            GridProduto.IsVisible = false;
            GridEletro.IsVisible = false;
            GridRoupa.IsVisible = false;
            GridMobilia.IsVisible = true;
            GridDinheiro.IsVisible = false;
            GridCestaBasica.IsVisible = false;
        }
        else if (pckTipoDoacao.SelectedIndex == 3)
		{
            GridProduto.IsVisible = false;
            GridEletro.IsVisible = false;
            GridRoupa.IsVisible = true;
            GridMobilia.IsVisible = false;
            GridDinheiro.IsVisible = false;
            GridCestaBasica.IsVisible = false;
        }
        else if (pckTipoDoacao.SelectedIndex == 4)
		{
            GridProduto.IsVisible = false;
            GridEletro.IsVisible = true;
            GridRoupa.IsVisible = false;
            GridMobilia.IsVisible = false;
            GridDinheiro.IsVisible = false;
            GridCestaBasica.IsVisible = false;
        }
        else if (pckTipoDoacao.SelectedIndex == 5)
        {
            GridProduto.IsVisible = false;
            GridEletro.IsVisible = false;
            GridRoupa.IsVisible = false;
            GridMobilia.IsVisible = false;
            GridDinheiro.IsVisible = false;
            GridCestaBasica.IsVisible = true;
        }
    }
}