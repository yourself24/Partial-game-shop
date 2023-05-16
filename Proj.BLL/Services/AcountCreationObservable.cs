using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class AccountCreationObservable : IAccountCreationObservable
    {
        private readonly List<IAccountCreationObserver> _observers = new List<IAccountCreationObserver>();

        public void RegisterObserver(IAccountCreationObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IAccountCreationObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(User user)
        {
            foreach (var observer in _observers)
            {
                observer.HandleAccountCreation(user);
            }
        }
    }

}
