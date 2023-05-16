using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IGenericService<TModel> where TModel : class
    {
        Task<List<TModel>> ReadAll();
        Task<TModel> ReadOne(int id);

        Task Create(TModel model);

        Task Update(TModel model);

        Task Delete(int id);
    }
}
