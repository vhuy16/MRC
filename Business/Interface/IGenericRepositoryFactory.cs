namespace Business.Interface
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
