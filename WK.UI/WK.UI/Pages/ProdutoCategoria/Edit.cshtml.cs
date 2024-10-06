using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Pages.ProdutoCategoria
{
    public class EditModel : PageModel
    {

        private readonly IWkClientService _wkClientService;

        public EditModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        [BindProperty]
        public ProdutoCategoriaView ProdutoCategoria { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var retorno = await _wkClientService.GetDataAsync($"api/ProdutoCategoria/ObterPorId/{id}");

            ProdutoCategoria = JsonSerializer.Deserialize<ProdutoCategoriaView>(retorno);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Se o estado do modelo não for válido, retorna a página com os erros
                return Page();
            }

            var retorno = await _wkClientService.PutDataAsync<ProdutoCategoriaView>("api/ProdutoCategoria/Atualizar", ProdutoCategoria);

            return RedirectToPage("./Index");
        }
    }
}
