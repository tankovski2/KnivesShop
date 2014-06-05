using KnivesShop.Models;

namespace KnivesShop.Data
{
    public interface IUowData
    {
        IRepository<Article> Articles { get; }

        IRepository<Category> Categories { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();

        void Dispose();
    }
}
