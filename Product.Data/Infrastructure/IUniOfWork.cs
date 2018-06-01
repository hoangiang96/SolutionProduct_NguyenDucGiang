namespace Product.Data.Infrastructure
{
    public interface IUniOfWork
    {
        void Commit();
    }
}