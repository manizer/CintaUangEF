using Model.Domain.DB;
using Model.Domain.DB.UserDB;
using Model.DTO.DB;
using Model.DTO.DB.UserDB;
using Repository.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modules
{
    public interface IUserService
    {
        Task<User> Login(User user);
        Task<ExecuteResult> Register(User user, int AuditedUserId);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Login(User user)
        {
            UserDTO userDTO = await userRepository.Login(user.UserEmail, user.UserPassword);

            return (userDTO == null) ? null : new User
            {
                UserId = userDTO.UserId,
                UserName = userDTO.UserName,
                UserEmail = userDTO.UserEmail,
                UserPassword = userDTO.UserPassword
            };
        }

        public async Task<ExecuteResult> Register(User user, int AuditedUserId)
        {
            ExecuteResultDTO executeResultDTO = await userRepository.Register
            (
                new UserDTO
                {
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword
                },
                AuditedUserId
            );

            return (executeResultDTO == null) ? null : new ExecuteResult
            {
                InstanceId = executeResultDTO.InstanceId
            };
        }
    }
}
