using APICatalogo.Context;
using APICatalogo.Interfaces;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriaRepository ? _categoriaRepositorio;
        private IProdutoRepository ? _produtoRepository;
        
        public AppDbContext _contexto;

        public UnitOfWork(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepositorio = _categoriaRepositorio ?? new CategoriaRepository(_contexto);
            }
        }

        public IProdutoRepository ProdutoRepository 
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_contexto); 
            }
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public void Dispose() 
        { 
            _contexto.Dispose();
        }
    }
}
