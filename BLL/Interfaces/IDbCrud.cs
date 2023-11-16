using System.Collections.Generic;

namespace BLL
{
    public interface IDbCrud
    {
        List<ProductModel> GetAllProduct();
        List<OrderModel> GetAllOrders();
        List<ManufacturerModel> GetAllManufacturers();
        List<CategoryModel> GetAllCategories();
        List<CustomerModel> GetAllCustomers();
        List<SellerModel> GetAllSellers();
        ProductModel GetProduct(int id);
        int CreateProduct(ProductModel p);
        void UpdateProduct(ProductModel p);
        void DeleteProduct(int id);
        void DeleteOrder(int id);
        void UpdateOrderLine(OrderLineModel o);
        bool AddOrderLine(OrderLineModel o);
        OrderModel CreateBusket();
        void DeleteOrderLine(int orderId, int productId);

    }
}
