using KcmsChallengeAPP.Models;
using KcmsChallengeAPP.ViewModels;
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
    public partial class RegistrarProdutoView : ContentPage
    {
        public RegistrarProdutoView()
        {
            InitializeComponent();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var _categoria = (sender as Picker)?.SelectedItem as Categoria;
            (BindingContext as RegistrarProdutoViewModel).ProdutoModel.CategoriaID = _categoria.CategoriaID;
        }
    }
}