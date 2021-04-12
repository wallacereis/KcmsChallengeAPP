using Acr.UserDialogs;
using KcmsChallengeAPP.Helpers;
using KcmsChallengeAPP.Interfaces;
using KcmsChallengeAPP.Models;
using KcmsChallengeAPP.ViewModels.Base;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class RegistrarProdutoViewModel : BaseViewModel, IInitializationAsync
    {
        #region [Properties]
        /*---------------------- Categorias Properties ----------------------*/
        private ObservableCollection<Categoria> _categoria;
        public ObservableCollection<Categoria> Categorias
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }
        /*---------------------- ImageSource Properties ----------------------*/
        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }
        /*---------------------- Preco Properties ----------------------*/
        private decimal _preco;
        public decimal Preco
        {
            get { return _preco; }
            set { SetProperty(ref _preco, value); }
        }
        /*---------------------- PrecoPromocional Properties ----------------------*/
        private decimal _precoPromocional;
        public decimal PrecoPromocional
        {
            get { return _precoPromocional; }
            set { SetProperty(ref _precoPromocional, value); }
        }
        public Task InitializeAsync { get; }
        private FileResult _mediaFile;
        public IAsyncCommand ConfirmarCommand { get; }
        public IAsyncCommand PickPhotoCommand { get; }
        public Produto ProdutoModel { get; set; }

        #endregion

        public RegistrarProdutoViewModel()
        {
            Categorias = new ObservableCollection<Categoria>();
            PickPhotoCommand = new AsyncCommand(ExecutePickPhotoCommandAsync);
            ConfirmarCommand = new AsyncCommand(ExecuteConfirmarCommandAsync, allowsMultipleExecutions: false);
            ProdutoModel = new Produto
            {
                Descricao = string.Empty,
                Detalhes = string.Empty,
                Preco = 0,
                PrecoPromocional = 0,
                CategoriaID = string.Empty
            };
            InitializeAsync = InitializationAsync();
        }

        private async Task InitializationAsync()
        {
            var _produto = JsonConvert.DeserializeObject<Produto>(SettingsPreferences.GetValue("Produto", ""));
            if (_produto == null)
                ImageSource = "no_image.png";
            await LoadCategoriasAsync();
        }

        private async Task ExecutePickPhotoCommandAsync()
        {
            try
            {
                _mediaFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Anexar uma imagem"
                });
                if (_mediaFile != null)
                {
                    var stream = await _mediaFile.OpenReadAsync();
                    ImageSource = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ooops!", "Ocorreu algo inesperado!" + Environment.NewLine + "Por favor, tente novamente!", "OK");
            }
        }

        private async Task ExecuteConfirmarCommandAsync()
        {
            if (!ValidaCampos())
            {
                //Toast Messages
                UserDialogs.Instance.Toast("Campos obrigatórios!", TimeSpan.FromSeconds(1));
                return;
            }
            if (String.IsNullOrWhiteSpace(ProdutoModel.CategoriaID))
            {
                //Toast Messages
                UserDialogs.Instance.Toast("Por favor, selecione uma Categoria!", TimeSpan.FromSeconds(1));
                return;
            }
            var _produto = JsonConvert.DeserializeObject<Produto>(SettingsPreferences.GetValue("Produto", ""));
            if (_produto is null && _mediaFile is null)
            {
                //Toast Messages
                UserDialogs.Instance.Toast("Por favor, anexar uma imagem!", TimeSpan.FromSeconds(1));
                return;
            }
            await RegistrarProdutoAsync();
        }

        private async Task RegistrarProdutoAsync()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var _realmDB = Realm.GetInstance();
                    using (UserDialogs.Instance.Loading("Registrando Produto...", null, null, true, MaskType.Gradient))
                    {
                        //Buscar Produto
                        var _produto = JsonConvert.DeserializeObject<Produto>(SettingsPreferences.GetValue("Produto", ""));
                        if (!string.IsNullOrWhiteSpace(PrecoPromocional.ToString()))
                            _precoPromocional = PrecoPromocional;
                        if (_produto == null)
                        {
                            //Registrar Produto
                            Produto objProduto = new Produto()
                            {
                                Descricao = ProdutoModel.Descricao.Trim(),
                                Detalhes = ProdutoModel.Detalhes.Trim(),
                                Preco = Preco,
                                PrecoPromocional = _precoPromocional,
                                Imagem = _mediaFile.FileName,
                                ImagemURL = _mediaFile.FullPath,
                                DataCadastro = System.DateTime.Now,
                                Ativo = true,
                                CategoriaID = ProdutoModel.CategoriaID
                            };
                            //------------ Registrar Produto ------------/
                            _realmDB.Write(() =>
                            {
                                _realmDB.Add(objProduto);
                            });
                            SettingsPreferences.SetValue("Mensagem", "Produto cadastrado com sucesso!");
                        }
                        else
                        {
                            //Atualizar Produto
                            var objProduto = _realmDB.Find<Produto>(_produto.ProdutoID);
                            using (var db = _realmDB.BeginWrite())
                            {
                                objProduto.Descricao = ProdutoModel.Descricao.Trim();
                                objProduto.Detalhes = ProdutoModel.Detalhes.Trim();
                                objProduto.Preco = Preco;
                                objProduto.PrecoPromocional = _precoPromocional;
                                if(_mediaFile is not null)
                                {
                                    objProduto.Imagem = _mediaFile.FileName;
                                    objProduto.ImagemURL = _mediaFile.FullPath;
                                }
                                objProduto.DataCadastro = System.DateTime.Now;
                                objProduto.Ativo = true;
                                objProduto.CategoriaID = ProdutoModel.CategoriaID;
                                db.Commit();
                            }
                            SettingsPreferences.SetValue("Mensagem", "Produto atualizado com sucesso!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    await Shell.Current.GoToAsync("Mensagem", true);
                }
                if (Error != null)
                {
                    IsBusy = false;
                    await DisplayAlert("Ooops!", "Ocorreu algo inesperado!" + Environment.NewLine + "Por favor, tente novamente!", "OK");
                }
            }
        }

        /*---------------------- Load Categorias Async ----------------------*/
        public async Task LoadCategoriasAsync()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    IsRefresh = true;
                    using (UserDialogs.Instance.Loading("Atualizando...", null, null, true, MaskType.Gradient))
                    {
                        //Buscar Produto
                        var _produto = JsonConvert.DeserializeObject<Produto>(SettingsPreferences.GetValue("Produto", ""));
                        if(_produto is not null)
                        {
                            ProdutoModel.Descricao = _produto.Descricao;
                            ProdutoModel.Detalhes = _produto.Detalhes;
                            Preco = _produto.Preco;
                            PrecoPromocional = _produto.PrecoPromocional;
                            ImageSource = _produto.ImagemURL;
                        }
                        //Carregar Categorias
                        var _realmDB = Realm.GetInstance();
                        var _listaCategorias = _realmDB.All<Categoria>().Where(c => c.Ativo == true).ToList();
                        Categorias = new ObservableCollection<Categoria>(_listaCategorias.OrderBy(c => c.Descricao));
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    IsRefresh = false;
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    IsRefresh = false;
                }
                if (Error != null)
                {
                    IsBusy = false;
                    IsRefresh = false;
                    await DisplayAlert("Ooops!", "Ocorreu algo inesperado!" + Environment.NewLine + "Por favor, tente novamente!", "OK");
                }
            }
        }

        private bool ValidaCampos()
        {
            if (String.IsNullOrWhiteSpace(ProdutoModel.Descricao.Trim()))
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            if (String.IsNullOrWhiteSpace(ProdutoModel.Detalhes.Trim()))
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            if (Preco == 0)
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            if (PrecoPromocional == 0)
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            IsSuccess = true;
            IsError = false;
            IsEmailError = false;
            return true;
        }
    }
}
