using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Aplicacao.Services;

namespace WK.UI.Pages.ProdutoCategoria
{
    public class IndexModel : PageModel
    {
        private readonly IWkClientService _wkClientService;

        public IndexModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        public void OnGet()
        {
            var teste = _wkClientService.GetDataAsync("api/ProdutoCategoria/ListaProdutoCategoria");
        }
    }
}
