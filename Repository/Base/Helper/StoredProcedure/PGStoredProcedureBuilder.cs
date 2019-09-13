using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace Repository.Base.Helper.StoredProcedure
{
    public class PGStoredProcedureBuilder : StoredProcedureBuilder
    {
        public override IStoredProcedureBuilder AddParam(object Value)
        {
            Params.Add(Value);
            return this;
        }

        public override StoredProcedure SP()
        {
            string QueryParamsTemplate = "";
            for (int i = 0; i < Params.Count; i++)
            {
                QueryParamsTemplate += "{" + i + "}";
                if (i != Params.Count - 1) QueryParamsTemplate += ", ";
            }
            string QueryStr = $"SELECT * FROM {SPName} ({QueryParamsTemplate})";
            StoredProcedure sp = new StoredProcedure
            {
                SP = QueryStr,
                args = Params.ToArray()
            };
            Dispose();
            return sp;
        }

        public override IStoredProcedureBuilder WithSPName(string StoredProcedureName)
        {
            SPName = StoredProcedureName;
            return this;
        }
    }
}
