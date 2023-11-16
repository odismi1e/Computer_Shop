
namespace DAL
{
    public interface IDbRepos
    {
        IRepository<Product> Products { get; }
        IRepository<OrderC> Orders { get; }
        IRepository<Category> Categories { get; }
        IRepository<Manufacturer> Manufacturers { get; }
        IRepository<Seller> Sellers { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Order_line> OrderLines { get; }
        IRepository<OrderStatus> Statuses { get; }
        IRepository<Supply> Supplies { get; }
        IRepository<SupplyLine> SupplyLines { get; }
        IReportsRepositoty Reports { get; }     
        int Save();

    }
}
