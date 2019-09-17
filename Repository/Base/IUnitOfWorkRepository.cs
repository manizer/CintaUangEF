using System;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Base.Helper
{
    public interface IUnitOfWorkRepository
    {
        void UseContext(DbContext context);
        void RevertToPreviousDbContext();
    }
}
