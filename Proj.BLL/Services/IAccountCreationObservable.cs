using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public interface IAccountCreationObservable
    {
        void RegisterObserver(IAccountCreationObserver observer);
        void RemoveObserver(IAccountCreationObserver observer);
        void NotifyObservers(User user);
    }

}
