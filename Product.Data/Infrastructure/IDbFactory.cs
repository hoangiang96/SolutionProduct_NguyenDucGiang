using System;

namespace Product.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProductDbContext Init();
    }
}