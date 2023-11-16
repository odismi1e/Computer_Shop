using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public struct UserInfo
    {
        public UserType type;
        public int id;
        public string Name;
    }
    public class AutorizationService : IAutorizationService
    {
        IDbRepos context;
        UserInfo curUser;

        public AutorizationService(IDbRepos context)
        {
            this.context = context;
            curUser = new UserInfo { type = UserType.Unauthorized, id = -1, Name = "" };
        }

        public UserInfo GetCurrentUser()
        {
            return curUser;
        }

        public bool TryAuthorization(string login, string password)
        {
            Customer c =  context.Customers.GetList().Where(i => i.login == login).FirstOrDefault();
            if (c != null)
            {
                if (c.password == password)
                {
                    curUser.type = UserType.Customer;
                    curUser.id = c.id;
                    curUser.Name = c.name;
                    return true;
                }

                return false;
            }

            Seller s = context.Sellers.GetList().Where(i => i.login == login).FirstOrDefault();

            if (s != null)
            {
                if (s.password == password)
                {
                    curUser.type = UserType.Seller;
                    curUser.id = s.id;
                    curUser.Name = s.name;
                    return true;
                }

                return false;
            }

            return false;
        }

        public void LogOut()
        {
            curUser.type = UserType.Unauthorized;
            curUser.id = -1;
        }

        public void CreateCustomer(string name, string sername, string login, string pass)
        {
            Customer c = new Customer
            {
                login = login,
                password = pass,
                name = name,
                surname = sername,
                OrderC = null,
                id = context.Customers.GetList().OrderByDescending(i => i.id).FirstOrDefault() == null ? 0 : context.Customers.GetList().OrderByDescending(i => i.id).FirstOrDefault().id + 1
            };

            context.Customers.Create(c);
            context.Save();
        }
    }
}
