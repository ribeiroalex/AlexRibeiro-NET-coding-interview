using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Core.Services;

public class BaseService<TEntity> : IService<TEntity>
    where TEntity : class
{
    private readonly IRepository<TEntity> _repository;

    public BaseService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        return OperationResult<IReadOnlyList<TEntity>>.Success(await _repository.GetAllAsync());
    }

    public async Task<OperationResult<IReadOnlyList<TEntity>>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return OperationResult<IReadOnlyList<TEntity>>.Success(await _repository.FilterAsync(predicate));
    }

    public async Task<OperationResult<TEntity>> FindAsync(params object[] keyValues)
    {
        var entity = await _repository.GetByIdAsync(keyValues);
        return entity is null ?
            OperationResult<TEntity>.NotFound($"Entity with key values {string.Join(", ", keyValues)} was not found") :
            OperationResult<TEntity>.Success(entity);
    }
}