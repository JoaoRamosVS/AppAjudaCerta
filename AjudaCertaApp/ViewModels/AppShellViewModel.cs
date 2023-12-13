using AjudaCertaApp.Models;
using AjudaCertaApp.Models.Enuns;
using AjudaCertaApp.Services.Pessoas;
using AjudaCertaApp.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjudaCertaApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private UsuarioService uService;
        private PessoaService pService;
        public AppShellViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuarioService(token);
            pService = new(token);

            CarregarUsuario();
        }

        #region AtributosPropriedades

        private TipoPessoaEnum tipo;
        public TipoPessoaEnum Tipo
        {
            get { return tipo; } 
            set
            {
                tipo = value;
                OnPropertyChanged();
            }
        }

        private string tipoPessoa;
        public string TipoPessoa
        {
            get { return tipoPessoa; }
            set
            {
                tipoPessoa = value;
                OnPropertyChanged();
            }
        }

        private byte[] foto;
        public byte[] Foto
        {
            get { return foto; } 
            set 
            {
                foto = value;
                OnPropertyChanged();
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Métodos

        public async void CarregarUsuario()
        {
            try
            {
                int usuarioId = Preferences.Get("UsuarioId", 0);
                Usuario u = await uService.GetUsuarioAsync(usuarioId);
                Pessoa p = await pService.GetPessoaPorUsuarioAsync();

                Foto = u.Foto;
                Username = p.Username;
                Tipo = p.Tipo;

                if (Tipo == TipoPessoaEnum.DOADOR)
                    TipoPessoa = "DOADOR";
                else if (Tipo == TipoPessoaEnum.ONG)
                    TipoPessoa = "ONG";
                else if (Tipo == TipoPessoaEnum.BENEFICIARIO)
                    TipoPessoa = "BENEFICIÁRIO";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
