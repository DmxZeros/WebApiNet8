﻿using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Interfaces
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
        PagedList<Categoria> GetCategoriasFiltrarNome(CategoriasFiltroNome categoriasFiltroNome);
    }
}
