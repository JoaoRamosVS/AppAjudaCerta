using AjudaCertaApp.Models;
using AjudaCertaApp.Services.Pessoas;
using AjudaCertaApp.Services.Posts;
using AjudaCertaApp.Views.Ong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AjudaCertaApp.ViewModels.Posts
{
    public class PostViewModel : BaseViewModel
    {
        private PostService pService;
        private PessoaService pessoaService;
        private string _token;
        private Post aPostar;
        public ICommand FotografarCommand { get; }
        public ICommand AbrirGaleriaCommand { get; }
        public ICommand SalvarImagemCommand { get; }
        public ICommand DirecionarCompartilharCommand { get; set; }
        public ICommand PostarCommand { get; set; }

        public PostViewModel()
        {
            _token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new(_token);
            FotografarCommand = new Command(Fotografar);
            AbrirGaleriaCommand = new Command(AbrirGaleria);
            InicializarCommands();
        }

        public PostViewModel(Post p, ImageSource i)
        {
            _token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new(_token);
            pessoaService = new(_token);
            aPostar = p;
            FonteImagem = i;
            InicializarCommands();
        }

        public void InicializarCommands() 
        {
            DirecionarCompartilharCommand = new Command(async () => await DirecionarCompartilhar());
            PostarCommand = new Command(async () => await Postar());
        }

        #region AtributosPropriedades

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

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;
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
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

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

        public async Task DirecionarCompartilhar()
        {
            try
            {
                Post p = new();
                p.Foto = foto;
                if (p.Foto != null && FonteImagem != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new compartilharPost(p, FonteImagem));
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Ops", "Selecione uma foto para prosseguir.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task Postar()
        {
            try
            {
                Post novoPost = aPostar;
                novoPost.Conteudo = Descricao;
                novoPost.DataPostagem = DateTime.Now;

                if (novoPost.Conteudo != null)
                {
                    int idPost = await pService.PostPostAsync(novoPost);
                    if (idPost != 0)
                    {
                        Pessoa pessoaLogada = await pessoaService.GetPessoaPorId(Preferences.Get("UsuarioId", 0));
                        Application.Current.MainPage = new AppShell(pessoaLogada);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Erro", "Tente novamente mais tarde", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Ops", "A descrição do post não pode estar vazia.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
