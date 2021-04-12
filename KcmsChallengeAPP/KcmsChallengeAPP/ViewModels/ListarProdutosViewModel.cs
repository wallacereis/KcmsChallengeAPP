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
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class ListarProdutosViewModel : BaseViewModel
    {
        #region [Properties]
        private ObservableCollection<Produto> _produto;
        public ObservableCollection<Produto> Produtos
        {
            get { return _produto; }
            set { SetProperty(ref _produto, value); }
        }
        /*---------------------- DescricaoCategoria Properties ----------------------*/
        private string _descricaoCategoria;
        public string DescricaoCategoria
        {
            get { return _descricaoCategoria; }
            set { SetProperty(ref _descricaoCategoria, value); }
        }
        public Task InitializeAsync { get; }
        public IAsyncCommand<Produto> SelectionChangedCommand { get; }
        public IAsyncCommand VoltarCommand { get; }
        public IAsyncCommand RefreshCommand { get; }
        #endregion

        public ListarProdutosViewModel()
        {
            SelectionChangedCommand = new AsyncCommand<Produto>((Produto obj) => ExecuteSelectionChangedCommandAsync(obj), allowsMultipleExecutions: false);
            VoltarCommand = new AsyncCommand(ExecuteVoltarCommandAsync, allowsMultipleExecutions: false);
            RefreshCommand = new AsyncCommand(ExecuteRefreshCommandAsync, allowsMultipleExecutions: false);
            InitializeAsync = InitializationAsync();
        }

        private async Task InitializationAsync()
        {
            await LoadProdutosAsync();
        }

        private async Task ExecuteRefreshCommandAsync()
        {
            await LoadProdutosAsync();
        }

        private async Task ExecuteSelectionChangedCommandAsync(Produto obj)
        {
            SettingsPreferences.SetValue("Produto", JsonConvert.SerializeObject(obj));
            var _editar = await DisplayAlert("Atenção!", "Selecione", "Editar", "Detalhes");
            if (_editar)
                await Shell.Current.GoToAsync("RegistrarProduto");
            else
                await Shell.Current.GoToAsync("ProdutoDetalhe");
        }

        private async Task ExecuteVoltarCommandAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        /*---------------------- Load Produtos ----------------------*/
        public async Task LoadProdutosAsync()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var _categoria = JsonConvert.DeserializeObject<Categoria>(SettingsPreferences.GetValue("Categoria", ""));
                    DescricaoCategoria = $"Lista de Produtos [{_categoria.Descricao}]";
                    var _realmDB = Realm.GetInstance();
                    var _listaProdutos = _realmDB.All<Produto>().Where(p => p.CategoriaID == _categoria.CategoriaID).ToList();
                    Produtos = new ObservableCollection<Produto>(_listaProdutos.OrderBy(c => c.Descricao));
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
