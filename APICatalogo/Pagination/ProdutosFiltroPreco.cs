namespace APICatalogo.Pagination
{
    public class ProdutosFiltroPreco: QueryStringParameters
    {
        public decimal? Preco { get; set; }

        // maior, menor ou igual
        public string? PrecoCriterio { get; set; }
    }
}
