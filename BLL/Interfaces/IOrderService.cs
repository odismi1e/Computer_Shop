namespace BLL
{
    public interface IOrderService
    {
        bool MakeOrderFromModel(OrderModel orderDto, IAutorizationService autorizationService);
        bool TakeOrder(int id, IAutorizationService autorizationService);
        bool CancelOrder(int id, IAutorizationService autorizationService);
        bool VerifyOrder(int id, IAutorizationService autorizationService);

    }
}
