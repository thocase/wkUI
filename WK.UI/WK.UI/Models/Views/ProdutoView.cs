namespace WK.UI.Models.Views
{
    public class ProdutoView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public ProdutoCategoriaView Categoria { get; set; }
    }
}
