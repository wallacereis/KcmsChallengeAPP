using KcmsChallengeAPP.Views;
using Xamarin.Forms;
namespace KcmsChallengeAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //------------------------ Register Shell Routers ------------------------ 
            Routing.RegisterRoute("HomePage", typeof(HomePageView));
            Routing.RegisterRoute("RegistrarCategoria", typeof(RegistrarCategoriaView));
            Routing.RegisterRoute("RegistrarProduto", typeof(RegistrarProdutoView));
            Routing.RegisterRoute("ListarCategorias", typeof(ListarCategoriasView));
            Routing.RegisterRoute("ListarProdutos", typeof(ListarProdutosView));
            Routing.RegisterRoute("ProdutoDetalhe", typeof(ProdutoDetalheView));
            Routing.RegisterRoute("Mensagem", typeof(MensagemView));
            Routing.RegisterRoute("SemConexao", typeof(SemConexaoView));
        }
    }
}
