using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Pages.Produto
{
    public class CreateModel : PageModel
    {
        private readonly IWkClientService _wkClientService;

        public CreateModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        [BindProperty]
        public ProdutoView Produto { get; set; }
        [BindProperty]
        public List<SelectListItem> ProdutoCategoria { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var retorno = await _wkClientService.GetDataAsync("api/ProdutoCategoria/ListaProdutoCategoria");


            if (retorno != null)
            {

                var listaCategoria = JsonSerializer.Deserialize<List<ProdutoCategoriaView>>(retorno);

                ProdutoCategoria = listaCategoria.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.nome
                }).ToList();

            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var retorno = await _wkClientService.PostDataAsync<ProdutoView>("api/Produto/Adicionar", Produto);

            return RedirectToPage("./Index");
        }
    }
}
