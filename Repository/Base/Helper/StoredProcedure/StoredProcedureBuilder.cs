using System;
using System.Collections.Generic;

namespace Repository.Base.Helper.StoredProcedure
{
    public abstract class StoredProcedureBuilder : IStoredProcedureBuilder
    {
        protected string SPName;
        protected List<object> Params = new List<object>();

        public virtual IStoredProcedureBuilder AddParam(object Value)
        {
            throw new NotImplementedException();
        }

        public virtual StoredProcedure SP()
        {
            throw new NotImplementedException();
        }

        public virtual IStoredProcedureBuilder WithSPName(string StoredProcedureName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            SPName = null;
            Params = new List<object>();
        }
    }
}
