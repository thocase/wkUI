namespace WK.UI.Models.Views
{
    public class ProdutoView
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public int quantidadeEstoque { get; set; }
        public int categoriaId { get; set; }

    }
}
