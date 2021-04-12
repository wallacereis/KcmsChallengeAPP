using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KcmsChallengeAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemConexaoView : ContentPage
    {
        public SemConexaoView()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}