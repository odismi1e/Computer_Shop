using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public enum UserType { Unauthorized = 0, Customer, Seller}
    public interface IAutorizationService
    {
        UserInfo GetCurrentUser();

        bool TryAuthorization(string login, string password);

        void LogOut();

        void CreateCustomer(string name, string sername, string login, string pass);
    }
}
