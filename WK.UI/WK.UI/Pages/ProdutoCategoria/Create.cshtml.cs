using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Pages.ProdutoCategoria
{
    public class CreateModel : PageModel
    {

        private readonly IWkClientService _wkClientService;

        public CreateModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        [BindProperty]
        public ProdutoCategoriaView ProdutoCategoria { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var retorno = await _wkClientService.PostDataAsync<ProdutoCategoriaView>("api/ProdutoCategoria/Adicionar", ProdutoCategoria);

            return RedirectToPage("./Index");
        }
    }
}
