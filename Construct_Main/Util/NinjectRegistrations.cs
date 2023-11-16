using Ninject.Modules;
using BLL;

namespace Construct_04
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IReportService>().To<ReportService>();
            Bind<IDbCrud>().To<DBDataOperation>();
            Bind<IAutorizationService>().To<AutorizationService>();
            Bind<ISupplyService>().To<SupplyService>();
        }
    }
}
