using Product.Data.Infrastructure;
using Product.Model.Models;

namespace Product.Data.Repositories
{
    public interface IFooterRepository:IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}