namespace APICatalogo.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IProdutoRepository ProdutoRepository { get; }
        Task CommitAsync();
        void Dispose(); 
    }
}
