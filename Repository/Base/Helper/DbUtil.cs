using System;
using Repository.Base.Helper.StoredProcedure;

namespace Repository.Base.Helper
{
    public class DbUtil
    {
        public IStoredProcedureBuilder StoredProcedureBuilder { get; private set; }

        public DbUtil(IStoredProcedureBuilder storedProcedureBuilder)
        {
            this.StoredProcedureBuilder = storedProcedureBuilder;
        }

        public void DisposeAll()
        {
            StoredProcedureBuilder.Dispose();
        }
    }
}
