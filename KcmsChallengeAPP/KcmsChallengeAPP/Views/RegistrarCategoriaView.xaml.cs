using KcmsChallengeAPP.Helpers;
using KcmsChallengeAPP.Models;
using Newtonsoft.Json;
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
    public partial class RegistrarCategoriaView : ContentPage
    {
        public RegistrarCategoriaView()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}