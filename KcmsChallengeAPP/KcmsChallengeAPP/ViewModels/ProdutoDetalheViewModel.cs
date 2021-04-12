using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using KcmsChallengeAPP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using KcmsChallengeAPP.Models;
using KcmsChallengeAPP.Helpers;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    public class ProdutoDetalheViewModel : BaseViewModel
    {
        #region [Properties]
        /*---------------------- ImagemURL Properties ----------------------*/
        private string _imagemURL;
        public string ImagemURL
        {
            get { return _imagemURL; }
            set { SetProperty(ref _imagemURL, value); }
        }
        /*---------------------- DescricaoProduto Properties ----------------------*/
        private string _descricaoProduto;
        public string DescricaoProduto
        {
            get { return _descricaoProduto; }
            set { SetProperty(ref _descricaoProduto, value); }
        }
        /*---------------------- DetalhesProduto Properties ----------------------*/
        private string _detalhesProduto;
        public string DetalhesProduto
        {
            get { return _detalhesProduto; }
            set { SetProperty(ref _detalhesProduto, value); }
        }
        /*---------------------- Preco Properties ----------------------*/
        private decimal _preco;
        public decimal Preco
        {
            get { return _preco; }
            set { SetProperty(ref _preco, value); }
        }
        /*---------------------- PrecoPromocional Properties ----------------------*/
        private decimal _precoPromocional;
        public decimal PrecoPromocional
        {
            get { return _precoPromocional; }
            set { SetProperty(ref _precoPromocional, value); }
        }
        public IAsyncCommand VoltarCommand { get; }
        #endregion

        public ProdutoDetalheViewModel()
        {
            VoltarCommand = new AsyncCommand(ExecuteVoltarCommandAsync, allowsMultipleExecutions: false);
            var _produto = JsonConvert.DeserializeObject<Produto>(SettingsPreferences.GetValue("Produto", ""));
            DescricaoProduto = _produto.Descricao;
            DetalhesProduto = _produto.Detalhes;
            Preco = _produto.Preco;
            PrecoPromocional = _produto.PrecoPromocional;
            ImagemURL = string.IsNullOrWhiteSpace(_produto.ImagemURL) ? "no_image.png" : _produto.ImagemURL;
        }

        private async Task ExecuteVoltarCommandAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
