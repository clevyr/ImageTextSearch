using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageTextSearch.Data
{
  public class BaseService<T> where T : class
  {
    protected readonly ApplicationDbContext Context;
    public BaseService(ApplicationDbContext context)
    {
      Context = context;
    }

    public virtual List<T> GetAll()
    {
      return Context.Set<T>().ToList();
    }

    public virtual Task<List<T>> GetAllAsync(int? skip, int? take)
    {
      var query = Context.Set<T>().Where(x => true);
      if (skip.HasValue) query = query.Skip(skip.Value);
      if (take.HasValue) query = query.Take(take.Value);
      return query.ToListAsync();
    

    }

    public virtual Task<T> FindAsync<KeyType>(KeyType id)
    {
      return Context.FindAsync<T>(id);
    }

    public virtual async Task<T> CreateAsync(T model)
    {
      Context.Set<T>().Add(model);
      await Context.SaveChangesAsync();
      return model;
    }

    public virtual async Task<T> DeleteAsync(T model)
    {
      Context.Set<T>().Remove(model);
      await Context.SaveChangesAsync();
      return model;
    }

    public virtual Task<T> GetRandom()
    {
      return Context.Set<T>().OrderBy(r => Guid.NewGuid()).FirstAsync();

    }
  }
}
