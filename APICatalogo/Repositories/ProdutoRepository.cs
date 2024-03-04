using APICatalogo.Context;
using APICatalogo.Interfaces;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto): base(contexto)
        {

        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            /*
            IQueryable<T> é apropriado quando você deseja realizar consultas de forma
            eficiente em uma fonte de dados que pode ser consultada diretamente, como
            um banco de dados.Ele suporta a consulta diferida e permite que as
            consultas sejam traduzidas em consultas SQL eficientes quando você está
            trabalhando com um provedor de banco de dados, como o Entity Framework.
            ------------------------------------------------------------------------
            IEnumerable < T > é uma interface mais geral que representa uma coleção de
            objetos em memória.Ela não oferece suporte a consultas diferidas ou tradução
            de consultas SQL.Isso significa que, ao usar IEnumerable, você primeiro traz
            todos os dados para a memória e, em seguida, aplica consultas, o que pode ser
            menos eficiente para grandes conjuntos de dados.
            */

            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();

            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos,
                produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = GetAll().AsQueryable();

            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }
            var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber,
                                                                                                  produtosFiltroParams.PageSize);
            return produtosFiltrados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}
