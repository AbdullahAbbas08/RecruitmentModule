using RM.BusinessLayer.IUnitOfWorkNameSpace;

namespace RM.BusinessLayer.IRepositories 
{
    #region Definitions
    public interface IGenericRepository<Entity> where Entity : class 
    {
        DbSet<Entity> DbSet { get; }

        Entity Add(Entity entity);

        Entity this[int id] { get; }

        Entity this[string id] { get; }

        Entity Find(int id);

        Entity Find(string id);
        int Count();

        IEnumerable<Entity> Get();

        IQueryable<Entity> GetAsNoTracking();
        Entity FirstOrDefault(Func<Entity, bool> predicate);
        IEnumerable<Entity> AddRange(IEnumerable<Entity> entities);


        Entity Remove(Entity entity);

        IEnumerable<Entity> RemoveRange(IEnumerable<Entity> entities);

        Entity Update(Entity entity);

        IEnumerable<Entity> Where(Func<Entity, bool> predicate);

        IQueryable<Entity> Query(Expression<Func<Entity, bool>> predicate);

        IQueryable<Entity> Skip(int count);

        IQueryable<Entity> Take(int count);

        IQueryable<Entity> Paging(int pageNumber, int PageSize);

        //IQueryable<Entity> Paging(string ordering, int pageNumber, int PageSize);
    }
    #endregion

    #region Implementation Section
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {

        private readonly DatabaseContext db;
        private readonly IUnitOfWork uow;

        public GenericRepository(DatabaseContext db, IUnitOfWork uow)
        {
            this.db = db;
            this.uow = uow;
        }

        public DbSet<Entity> DbSet
        {
            get
            {
                return db.Set<Entity>();
            }
        }

        public virtual Entity Add(Entity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public Entity this[int id] => Find(id);

        public Entity this[string id] => Find(id);


        public virtual Entity Find(int id)
        {
            return DbSet.Find(id);
        }

        public virtual Entity Find(string id)
        {
            return DbSet.Find(id);
        }


        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual Entity FirstOrDefault(Func<Entity, bool> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<Entity> AddRange(IEnumerable<Entity> entities)
        {
            DbSet.AddRange(entities);
            return entities;
        }


        public virtual IEnumerable<Entity> Get()
        {
            return DbSet.ToList();
        }

        public virtual IQueryable<Entity> GetAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public virtual Entity Remove(Entity entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public virtual IEnumerable<Entity> RemoveRange(IEnumerable<Entity> entities)
        {
            DbSet.RemoveRange(entities);
            return entities;
        }

        public virtual Entity Update(Entity entity)
        {
            DbSet.Update(entity);
            return entity;
        }


        public virtual IEnumerable<Entity> Where(Func<Entity, bool> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual IQueryable<Entity> Query(Expression<Func<Entity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }


        public virtual IQueryable<Entity> Skip(int count)
        {
            return DbSet.Skip(count);
        }

        public virtual IQueryable<Entity> Take(int count)
        {
            return DbSet.Take(count);
        }

        public virtual IQueryable<Entity> Paging(int pageNumber, int PageSize)
        {
            return DbSet.Skip((pageNumber - 1) * PageSize).Take(PageSize);
        }

        //public virtual IQueryable<Entity> Paging(string ordering, int pageNumber, int PageSize)
        //{
        //    return DbSet.OrderBy(ordering).Skip((pageNumber - 1) * PageSize).Take(PageSize);
        //}



    }
    #endregion

}
