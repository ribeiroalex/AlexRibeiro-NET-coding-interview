using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecureFlight.Core.Interfaces
{
    public interface IService<TEntity>
        where TEntity : class
    {
        Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync();
    }
}
