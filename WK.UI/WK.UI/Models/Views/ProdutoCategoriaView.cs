using System.ComponentModel.DataAnnotations;

namespace WK.UI.Models.Views
{
    public class ProdutoCategoriaView
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string nome { get; set; }
        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public string descricao { get; set; }
    }
}
