namespace Product.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ProductDbContext dbContext;

        public ProductDbContext Init()
        {
            return dbContext ?? (dbContext = new ProductDbContext());//kiemtra neu =null thi khoi tao
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}