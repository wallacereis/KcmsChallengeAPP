using KcmsChallengeAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KcmsChallengeAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MensagemView : ContentPage
    {
        public MensagemView()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}