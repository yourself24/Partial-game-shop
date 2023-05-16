using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class LogAccountCreationObserver : IAccountCreationObserver
    {
        private readonly string _logFilePath;

        public LogAccountCreationObserver()
        {
            // Get the log file path
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        }

        public void HandleAccountCreation(User user)
        {
            // Write to log file
            using (var writer = new StreamWriter(_logFilePath, true))
            {
                Console.WriteLine($"User account created: {user.Username}, {DateTime.Now}");
                Console.WriteLine(_logFilePath);
             
            }
        }
    }

}
