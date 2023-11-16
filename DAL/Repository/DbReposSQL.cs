namespace DAL
{
    public class DbReposSQL : IDbRepos
    {
        private ComputerShopContext context;
        private ProductRepositorySQL productRepository;
        private OrderRepositorySQL orderRepository;
        private CategoryRepositorySQL categoryRepository;
        private ManufacturerRepositorySQL manufacturerRepositorySQL;
        private ReportRepositorySQL reportRepository;
        private SellerRepositorySQL sellerRepository;
        private CustomerRepositotySQL customerRepositotySQL;
        private OrderLineRepositorySQL orderLineRepository;
        private OrderStatusRepositorySQL orderStatusRepository;
        private SupplyRepositorySQL supplyRepositorySQL;
        private SupplyLineRepositorySQL supplyLineRepositorySQL;
        public DbReposSQL()
        {
            context = new ComputerShopContext();
        }

        public IRepository<Product> Products
        {
            get
            {
                if(productRepository == null)
                    productRepository = new ProductRepositorySQL(context);
                return productRepository;
            }
        }

        public IRepository<OrderC> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepositorySQL(context);
                return orderRepository;
            }
        }

        public IRepository<Manufacturer> Manufacturers
        {
            get
            {
                if (manufacturerRepositorySQL == null)
                    manufacturerRepositorySQL = new ManufacturerRepositorySQL(context);
                return manufacturerRepositorySQL;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepositorySQL(context);
                return categoryRepository;
            }
        }
        public IRepository<Seller> Sellers
        {
            get
            {
                if (sellerRepository == null)
                    sellerRepository = new SellerRepositorySQL(context);
                return sellerRepository;
            }
        }
        public IRepository<Customer> Customers
        {
            get
            {
                if (customerRepositotySQL == null)
                    customerRepositotySQL = new CustomerRepositotySQL(context);
                return customerRepositotySQL;
            }
        }

        public IRepository<Order_line> OrderLines
        {
            get
            {
                if (orderLineRepository == null)
                    orderLineRepository = new OrderLineRepositorySQL(context);
                return orderLineRepository;
            }
        }

        public IRepository<OrderStatus> Statuses
        {
            get
            {
                if (orderStatusRepository == null)
                    orderStatusRepository = new OrderStatusRepositorySQL(context);
                return orderStatusRepository;
            }
        }

        public IRepository<Supply> Supplies
        {
            get
            {
                if (supplyRepositorySQL == null)
                    supplyRepositorySQL = new SupplyRepositorySQL(context);
                return supplyRepositorySQL;
            }
        }

        public IRepository<SupplyLine> SupplyLines
        {
            get
            {
                if (supplyLineRepositorySQL == null)
                    supplyLineRepositorySQL = new SupplyLineRepositorySQL(context);
                return supplyLineRepositorySQL;
            }
        }

        public IReportsRepositoty Reports
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(context);
                return reportRepository;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
