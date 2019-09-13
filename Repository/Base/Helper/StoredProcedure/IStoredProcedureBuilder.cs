using System;
namespace Repository.Base.Helper.StoredProcedure
{
    public interface IStoredProcedureBuilder : IDisposable
    {
        IStoredProcedureBuilder WithSPName(string StoredProcedureName);
        IStoredProcedureBuilder AddParam(object Value);
        StoredProcedure SP();
    }
}
