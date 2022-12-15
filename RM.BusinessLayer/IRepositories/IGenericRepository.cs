namespace RM.BusinessLayer.IRepositories
{
    public interface IGenericRepository<Entity> where Entity : class 
    {
        DbSet<Entity> DbSet { get; }
        Entity Add(Entity entity);
        Entity this[int id] { get; }
        Entity this[string id] { get; }
        IEnumerable<Entity> Get();
        Entity Remove(Entity entity);
        Entity Update(Entity entity);
        IEnumerable<Entity> Where(Func<Entity, bool> predicate);
        IQueryable<Entity> Query(Expression<Func<Entity, bool>> predicate);

        IQueryable<Entity> Skip(int count);
        IQueryable<Entity> Take(int count);
        IQueryable<Entity> Paging(int pageNumber, int PageSize);
        IQueryable<Entity> Paging(string ordering, int pageNumber, int PageSize);


    }
}
