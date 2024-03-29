﻿using APICatalogo.Context;
using APICatalogo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _contexto;

        public Repository(AppDbContext context)
        {
            _contexto = context;
        }
        public T Create(T entity)
        {
            _contexto.Set<T>().Add(entity);
            //_contexto.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            //_contexto.SaveChanges();
            return entity;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _contexto.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _contexto.Set<T>().AsNoTracking().ToListAsync();
        }

        public T Update(T entity)
        {
            //_contexto.Set<T>().Update(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
            //_contexto.SaveChanges();
            return entity;
        }
    }
}
