using Data.Models;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable
        where TEntity : class
    {
        public DataBase Db;

        #region IRepositoryBase<TEntity> Members


        public RepositoryBase()
        {
            if (Db == null)
            {
                Db = new DataBase();
            }
        }

        public RepositoryBase(DataBase _Db)
        {
            if (Db == null)
            {
                Db = new DataBase();
            }
            else
            {
                Db = _Db;
            }
        }

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }

        public TEntity GetById(int? id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().Where(predicate);
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Db.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
        #endregion
    }

    public static class conn
    {
        public const string connectionString = "Server=(local);Database=TCCSistemaAgengamento;Trusted_Connection=True;Trust Server Certificate=true";

        //public const string connectionString = "Server=mobmserve.database.windows.net;Database=MOBMDB;User Id=usr_application;Password=eD@4DvotEz";

    }
}