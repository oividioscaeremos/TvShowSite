namespace TvShowSite.Core.Abstractions.DataAbstractions.Common
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
