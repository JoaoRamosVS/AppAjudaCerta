using AjudaCertaApp.Services.Usuarios;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using AjudaCertaApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using AjudaCertaApp.Services.Pessoas;

namespace AjudaCertaApp.ViewModels.Usuarios
{
    public class ImagemUsuarioViewModel : BaseViewModel
    {
        private UsuarioService usuarioService;
        private PessoaService pService;

        public ImagemUsuarioViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            usuarioService = new UsuarioService(token);
            pService = new(token);

            FotografarCommand = new Command(Fotografar);
            SalvarImagemCommand = new Command(SalvarImagem);
            AbrirGaleriaCommand = new Command(AbrirGaleria);
        }

        public ICommand AbrirGaleriaCommand { get; }
        public ICommand FotografarCommand { get; }
        public ICommand SalvarImagemCommand { get; }

        #region Atributos/propriedades

        private ImageSource fonteImagem;

        public ImageSource FonteImagem
        {
            get { return fonteImagem; }
            set 
            { 
                fonteImagem = value;
                OnPropertyChanged();
            }
        }

        private byte[] foto;

        public byte[] Foto
        {
            get => foto;
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Métodos

        public async void Fotografar()
        {
            try
            {
                if(MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if(photo != null)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                await sourceStream.CopyToAsync(ms);

                                Foto = ms.ToArray();

                                fonteImagem = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }

        }

        public async void AbrirGaleria()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                    if (photo != null)
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                await sourceStream.CopyToAsync(ms);

                                Foto = ms.ToArray();

                                FonteImagem = ImageSource.FromStream(() => new MemoryStream(ms.ToArray()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }

        }

        public async void SalvarImagem()
        {
            try
            {
                Usuario u = new Usuario();
                u.Foto = foto;
                u.Id = Preferences.Get("UsuarioId", 0);

                if (await usuarioService.PutFotoUsuarioAsync(u) != 0)
                {
                    Pessoa pessoaLogada = await pService.GetPessoaPorId(u.Id);
                    if(pessoaLogada != null) 
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Dados salvos com sucesso!", "Ok");
                        Application.Current.MainPage = new AppShell(pessoaLogada);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Erro", "Tente novamente mais tarde", "Ok");
                }
                else { throw new Exception("Erro ao tentar atualizar imagem"); }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        #endregion
    }
}
