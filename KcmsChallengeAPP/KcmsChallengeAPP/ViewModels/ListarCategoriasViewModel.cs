using Acr.UserDialogs;
using KcmsChallengeAPP.Helpers;
using KcmsChallengeAPP.Interfaces;
using KcmsChallengeAPP.Models;
using KcmsChallengeAPP.ViewModels.Base;
using KcmsChallengeAPP.Views;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class ListarCategoriasViewModel : BaseViewModel, IInitializationAsync
    {
        #region [Properties]
        private ObservableCollection<Categoria> _categoria;
        public ObservableCollection<Categoria> Categorias
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }
        public Task InitializeAsync { get; }
        public IAsyncCommand<Categoria> SelectionChangedCommand { get; }
        public IAsyncCommand VoltarCommand { get; }
        public IAsyncCommand RefreshCommand { get; }
        #endregion

        public ListarCategoriasViewModel()
        {
            SelectionChangedCommand = new AsyncCommand<Categoria>((Categoria obj) => ExecuteSelectionChangedCommandAsync(obj), allowsMultipleExecutions: false);
            VoltarCommand = new AsyncCommand(ExecuteVoltarCommandAsync, allowsMultipleExecutions: false);
            RefreshCommand = new AsyncCommand(ExecuteRefreshCommandAsync, allowsMultipleExecutions: false);
            InitializeAsync = InitializationAsync();
        }

        private async Task InitializationAsync()
        {
            await LoadCategoriasAsync();
        }

        private async Task ExecuteRefreshCommandAsync()
        {
            await LoadCategoriasAsync();
        }

        private async Task ExecuteSelectionChangedCommandAsync(Categoria obj)
        {
            if(obj.Ativo == false)
            {
                await DisplayAlert("Atenção!", "Não é possível listar os Produtos, Categoria inativa!", "OK");
                return;
            }
            using (UserDialogs.Instance.Loading("Atualizando...", null, null, true, MaskType.Gradient))
            {
                await Task.Delay(500);
                SettingsPreferences.SetValue("Categoria", JsonConvert.SerializeObject(obj));
                await Shell.Current.GoToAsync("ListarProdutos");
            }
        }

        private async Task ExecuteVoltarCommandAsync()
        {
            await Shell.Current.GoToAsync("..");
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
