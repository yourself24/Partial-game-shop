using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public interface IIterator<T>
    {
        Task<bool> HasNextAsync();
        Task<T> NextAsync();
    }

}
