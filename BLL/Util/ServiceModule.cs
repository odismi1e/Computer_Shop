using Ninject.Modules;
using DAL;
namespace BLL
{
    public class ServiceModule : NinjectModule
    {
        private string conntectionString;

        public ServiceModule(string conntection)
        {
            conntectionString = conntection;
        }
        public override void Load()
        {
            Bind<IDbRepos>().To<DbReposSQL>().InSingletonScope().WithConstructorArgument(conntectionString);
        }
    }
}
