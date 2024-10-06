using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Aplicacao.Services;
using WK.UI.Models.Views;

namespace WK.UI.Pages.ProdutoCategoria
{
    public class IndexModel : PageModel
    {
        private readonly IWkClientService _wkClientService;

        public List<ProdutoCategoriaView> ProdutoCategoriaList { get; set; } = new List<ProdutoCategoriaView>();

        [BindProperty(SupportsGet = true)]
        public string FiltroList { get; set; }

        public IndexModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        public async Task OnGet()
        {
            var retorno = await _wkClientService.GetDataAsync("api/ProdutoCategoria/ListaProdutoCategoria");

            if (retorno != null)
            {
                ProdutoCategoriaList = JsonSerializer.Deserialize<List<ProdutoCategoriaView>>(retorno);

                if (!string.IsNullOrEmpty(FiltroList))
                {
                    ProdutoCategoriaList = ProdutoCategoriaList
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
            var retorno = await _wkClientService.DeleteDataAsync($"api/ProdutoCategoria/Delete/{id}");

            return RedirectToPage("./Index");
        }
    }
}
