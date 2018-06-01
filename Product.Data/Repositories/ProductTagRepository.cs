using Product.Data.Infrastructure;
using Product.Model.Models;

namespace Product.Data.Repositories
{
    public interface IItemTagRepository : IRepository<ProductTag>
    {
    }

    public class ProductTagRepository : RepositoryBase<ProductTag>, IItemTagRepository
    {
        public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}