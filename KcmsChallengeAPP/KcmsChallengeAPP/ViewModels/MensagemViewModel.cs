using KcmsChallengeAPP.Helpers;
using KcmsChallengeAPP.ViewModels.Base;
using KcmsChallengeAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class MensagemViewModel : BaseViewModel
    {
        #region "Properties"
        /*---------------------- Mensagem Properties ----------------------*/
        private string _mensagem;
        public string Mensagem
        {
            get { return _mensagem; }
            set { SetProperty(ref _mensagem, value); }
        }
        public IAsyncCommand FinalizarCommand { get; }
        #endregion

        public MensagemViewModel()
        {
            FinalizarCommand = new AsyncCommand(ExecuteFinalizarCommandAsync);
            Mensagem = SettingsPreferences.GetValue("Mensagem","");
        }

        private async Task ExecuteFinalizarCommandAsync()
        {
            await Shell.Current.Navigation.PopModalAsync();
            Preferences.Clear();
            await Shell.Current.GoToAsync("HomePage");
        }
    }
}
