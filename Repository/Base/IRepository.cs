using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;

namespace Repository.Base
{
    public interface IRepository<TEntity> : IUnitOfWorkRepository where TEntity : class
    {
        Task<IEnumerable<TEntity>> ExecSPToListAsync(StoredProcedure sp);
        Task<TEntity> ExecSPToSingleAsync(StoredProcedure sp);
        Task<IEnumerable<TEntityResult>> ExecSPWithTransaction<TEntityResult>(StoredProcedure[] storedProcedures) where TEntityResult : class;
    }
}