using Model.DTO.DB;
using Model.DTO.DB.UserDB;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<UserDTO>
    {
        Task<UserDTO> Login(string userEmail, string userPassword);
        Task<ExecuteResultDTO> Register(UserDTO user, int AuditedUserId);
    }
    public class UserRepository : BaseRepository<UserDTO>, IUserRepository
    {
        public UserRepository(CintaUangDbContext dbContext, DbUtil dbUtil) : base(dbContext, dbUtil) { }

        public async Task<UserDTO> Login(string userEmail, string userPassword)
        {
            var sp = DbUtil.StoredProcedureBuilder.WithSPName("msuser_doLogin")
                .AddParam(userEmail)
                .AddParam(userPassword)
                .SP();

            return await ExecSPToSingleAsync(sp);
        }

        public async Task<ExecuteResultDTO> Register(UserDTO user, int AuditedUserId)
        {
            List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
            storedProcedures.Add(
                DbUtil.StoredProcedureBuilder.WithSPName("msuser_insert")
                    .AddParam(user.UserName)
                    .AddParam(user.UserEmail)
                    .AddParam(user.UserPassword)
                    .AddParam(AuditedUserId)
                    .SP()
            );

            List<ExecuteResultDTO> executeResults = (await ExecSPWithTransaction<ExecuteResultDTO>(storedProcedures.ToArray())).ToList();
            return executeResults[0];
        }
    }
}
