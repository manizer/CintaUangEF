using System;
using Repository.Context;

namespace Repository.Base.Helper
{
    public interface IUnitOfWorkRepository
    {
        void UseContext(CintaUangDbContext context);
        void RevertToPreviousDbContext();
    }
}
