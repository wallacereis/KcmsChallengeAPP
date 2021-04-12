using KcmsChallengeAPP.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class SemConexaoViewModel : BaseViewModel
    {
        #region "Properties"
        public IAsyncCommand TentarNovamenteCommand { get; }
        #endregion

        public SemConexaoViewModel()
        {
            TentarNovamenteCommand = new AsyncCommand(ExecuteTentarNovamenteCommandAsync, allowsMultipleExecutions:false);
        }

        private async Task ExecuteTentarNovamenteCommandAsync()
        {
            if (!InternetConnectionActive())
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
