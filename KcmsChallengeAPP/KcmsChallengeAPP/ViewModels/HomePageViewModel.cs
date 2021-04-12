using Acr.UserDialogs;
using KcmsChallengeAPP.Helpers;
using KcmsChallengeAPP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class HomePageViewModel : BaseViewModel
    {
        #region [Properties]
        public IAsyncCommand RegistrarCategoriaCommand { get; }
        public IAsyncCommand RegistrarProdutoCommand { get; }
        public IAsyncCommand ListarCategoriaCommand { get; }
        public IAsyncCommand FaleConoscoCommand { get; }
        public Command LogoutCommand { get; }
        #endregion

        public HomePageViewModel()
        {
            RegistrarCategoriaCommand = new AsyncCommand(ExecuteRegistrarCategoriaCommandAsync, allowsMultipleExecutions: false);
            RegistrarProdutoCommand = new AsyncCommand(ExecuteRegistrarProdutoCommandAsync, allowsMultipleExecutions: false);
            ListarCategoriaCommand = new AsyncCommand(ExecuteListarCategoriaCommandAsync, allowsMultipleExecutions: false);
            FaleConoscoCommand = new AsyncCommand(ExecuteFaleConoscoCommandAsync, allowsMultipleExecutions: false);
            LogoutCommand = new Command(() => ExecuteLogoutCommandAsync(), () => !IsBusy);
        }

        private async Task ExecuteListarCategoriaCommandAsync()
        {
            using (UserDialogs.Instance.Loading("Atualizando...", null, null, true, MaskType.Gradient))
            {
                await Task.Delay(500);
                await Shell.Current.GoToAsync("ListarCategorias");
            }
        }

        private async Task ExecuteRegistrarCategoriaCommandAsync()
        {
            await Shell.Current.GoToAsync("RegistrarCategoria");
        }

        private async Task ExecuteRegistrarProdutoCommandAsync()
        {
            SettingsPreferences.DeleteValue("Produto");
            await Shell.Current.GoToAsync("RegistrarProduto");
        }

        private async Task ExecuteFaleConoscoCommandAsync()
        {
            if (!InternetConnectionActive())
            {
                await Shell.Current.GoToAsync("SemConexao", true);
                return;
            }
            var urlKcms = "https://www.kcms.com.br/contato/";
            await Browser.OpenAsync(urlKcms, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });
        }

        private void ExecuteLogoutCommandAsync()
        {
            Preferences.Clear();
            App.Current.MainPage = new AppShell();
        }
    }
}
