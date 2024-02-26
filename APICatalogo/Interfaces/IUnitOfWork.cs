namespace APICatalogo.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IProdutoRepository ProdutoRepository { get; }
        void Commit();
        void Dispose(); 
    }
}
