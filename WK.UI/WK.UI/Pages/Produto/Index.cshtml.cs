using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Pages.Produto
{
    public class IndexModel : PageModel
    {
        private readonly IWkClientService _wkClientService;

        public List<ProdutoView> ProdutoList { get; set; } = new List<ProdutoView>();

        [BindProperty]
        public List<SelectListItem> ProdutoCategoria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FiltroList { get; set; }

        public IndexModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        public async Task OnGet()
        {
            var retornoCategoria = await _wkClientService.GetDataAsync("api/ProdutoCategoria/ListaProdutoCategoria");

            if (retornoCategoria != null)
            {
                var listaCategoria = JsonSerializer.Deserialize<List<ProdutoCategoriaView>>(retornoCategoria);

                ProdutoCategoria = listaCategoria.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.nome
                }).ToList();
            }
                

            var retorno = await _wkClientService.GetDataAsync("api/Produto/ListaProduto");

            if (retorno != null)
            {

                ProdutoList = JsonSerializer.Deserialize<List<ProdutoView>>(retorno);

                if (!string.IsNullOrEmpty(FiltroList))
                {
                    ProdutoList = ProdutoList
                        .Where(x => x.nome.Contains(FiltroList) || x.descricao.Contains(FiltroList))
                        .ToList();
                } 
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var retorno = await _wkClientService.DeleteDataAsync($"api/Produto/Delete/{id}");

            return RedirectToPage("./Index");
        }
    }
}
