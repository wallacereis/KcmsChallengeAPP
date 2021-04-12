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
using System.Transactions;
using Xamarin.CommunityToolkit.ObjectModel;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class RegistrarCategoriaViewModel : BaseViewModel, IInitializationAsync
    {
        #region [Properties]
        /*---------------------- Categorias Properties ----------------------*/
        private ObservableCollection<Categoria> _categoria;
        public ObservableCollection<Categoria> Categorias
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }
        public Task InitializeAsync { get; }
        public IAsyncCommand<Categoria> SelectionChangedCommand { get; }
        public IAsyncCommand ConfirmarCommand { get; }
        public IAsyncCommand RefreshCommand { get; }
        public Categoria CategoriaModel { get; set; }
        #endregion

        public RegistrarCategoriaViewModel()
        {
            SelectionChangedCommand = new AsyncCommand<Categoria>((Categoria obj) => ExecuteSelectionChangedCommandAsync(obj), allowsMultipleExecutions: false);
            ConfirmarCommand = new AsyncCommand(ExecuteConfirmarCommandAsync, allowsMultipleExecutions: false);
            RefreshCommand = new AsyncCommand(ExecuteRefreshCommandAsync, allowsMultipleExecutions: false);
            CategoriaModel = new Categoria
            {
                Descricao = string.Empty,
                Ativo = false
            };
            InitializeAsync = InitializationAsync();
        }

        private async Task ExecuteSelectionChangedCommandAsync(Categoria obj)
        {
            var _editar = await DisplayAlert("Atenção!", "Editar esta Categoria?", "Sim", "Não");
            if (_editar)
            {
                SettingsPreferences.SetValue("Categoria", JsonConvert.SerializeObject(obj));
                CategoriaModel.Descricao = obj.Descricao;
                CategoriaModel.Ativo = obj.Ativo;
            }
        }

        private async Task InitializationAsync()
        {
            await LoadCategoriasAsync();
        }

        private async Task ExecuteRefreshCommandAsync()
        {
            await LoadCategoriasAsync();
        }

        private async Task ExecuteConfirmarCommandAsync()
        {
            if (string.IsNullOrWhiteSpace(CategoriaModel.Descricao))
            {
                //Toast Messages
                UserDialogs.Instance.Toast("Por favor, informe a Descrição!", TimeSpan.FromSeconds(1));
                return;
            }
            await RegistrarCategoriaAsync();
        }

        private async Task RegistrarCategoriaAsync()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var _realmDB = Realm.GetInstance();
                    using (UserDialogs.Instance.Loading("Registrando Categoria...", null, null, true, MaskType.Gradient))
                    {
                        //Buscar Categoria 
                        var _categoria = JsonConvert.DeserializeObject<Categoria>(SettingsPreferences.GetValue("Categoria", ""));
                        //Adicionar nova Categoria
                        if (_categoria == null)
                        {
                            Categoria objCategoria = new Categoria()
                            {
                                Descricao = CategoriaModel.Descricao.Trim(),
                                DataCadastro = System.DateTime.Now,
                                Ativo = CategoriaModel.Ativo,
                            };
                            _realmDB.Write(() =>
                            {
                                _realmDB.Add(objCategoria);
                            });
                            //Toast Messages
                            UserDialogs.Instance.Toast("Categoria cadastrada com sucesso!", TimeSpan.FromSeconds(1));
                        }
                        else
                        {
                            //Atualizar Categoria
                            var objCategoria = _realmDB.Find<Categoria>(_categoria.CategoriaID);
                            using (var db = _realmDB.BeginWrite())
                            {
                                objCategoria.Descricao = CategoriaModel.Descricao.Trim();
                                objCategoria.Ativo = CategoriaModel.Ativo;
                                db.Commit();
                            }
                            UserDialogs.Instance.Toast("Categoria atualizada com sucesso!", TimeSpan.FromSeconds(1));
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
                    await LoadCategoriasAsync();
                }
                if (Error != null)
                {
                    IsBusy = false;
                    await DisplayAlert("Ooops!", "Ocorreu algo inesperado!" + Environment.NewLine + "Por favor, tente novamente!", "OK");
                }
            }
        }

        /*---------------------- Load Categorias ----------------------*/
        public async Task LoadCategoriasAsync()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var _realmDB = Realm.GetInstance();
                    var _listaCategorias = _realmDB.All<Categoria>().ToList();
                    Categorias = new ObservableCollection<Categoria>(_listaCategorias.OrderBy(c => c.Descricao));
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    //Limpa os campos
                    CategoriaModel.Descricao = string.Empty;
                    CategoriaModel.Ativo = false;
                    SettingsPreferences.DeleteValue("Categoria");
                }
                if (Error != null)
                {
                    IsBusy = false;
                    await DisplayAlert("Ooops!", "Ocorreu algo inesperado!" + Environment.NewLine + "Por favor, tente novamente!", "OK");
                }
            }
        }
    }
}
