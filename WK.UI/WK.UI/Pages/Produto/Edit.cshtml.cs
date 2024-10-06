using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Pages.Produto
{
    public class EditModel : PageModel
    {
        private readonly IWkClientService _wkClientService;

        public EditModel(IWkClientService wkClientService)
        {
            _wkClientService = wkClientService;
        }

        [BindProperty]
        public ProdutoView Produto { get; set; }
        [BindProperty]
        public List<SelectListItem> ProdutoCategoria { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var retorno = await _wkClientService.GetDataAsync($"api/Produto/ObterPorId/{id}");

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

            Produto = JsonSerializer.Deserialize<ProdutoView>(retorno);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Se o estado do modelo não for válido, retorna a página com os erros
                return Page();
            }

            var retorno = await _wkClientService.PutDataAsync<ProdutoView>("api/Produto/Atualizar", Produto);

            return RedirectToPage("./Index");
        }
    }
}
