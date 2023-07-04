using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(bool state = true);
    }
}
