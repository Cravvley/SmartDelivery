using SmartDelivery.Data.Entities;
using SmartDelivery.Data.Repositories;
using SmartDelivery.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartDelivery.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(string id)
        {
            var userEntity = await _userRepository.Get(id);
            if (userEntity is null)
            {
                throw new ArgumentNullException("user doesn't exist");
            }

            return userEntity;
        }

        public async Task<IList<User>> GetAll(Expression<Func<User, bool>> filter = null)
                        => await _userRepository.GetAll(filter);

        public async Task<bool> Lock(string userId, bool lockUser)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
            {
                throw new ArgumentNullException("user doesn't exist");

            }

            if (lockUser)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                await _userRepository.Update(user);

                return true;
            }

            user.LockoutEnd = DateTime.Now;
            await _userRepository.Update(user);

            return false;
        }

        public async Task<IList<User>> GetPaginated(Expression<Func<User, bool>> filter = null, int pageSize = 1, int productPage = 1)
        {
            if (filter is null)
            {
                return await _userRepository.GetPaginated(p => true, pageSize, productPage);
            }

            return await _userRepository.GetPaginated(filter, pageSize, productPage);
        }

        public async Task<(IList<User> users, int usersCount)> GetFiltered(string userId, string userEmail = null, int? pageSize = null, int? productPage = null)
        {
            var users = await GetAll(u => u.Id != userId);

            if ((pageSize is null || productPage is null) && userEmail is null)
            {
                return (users, users.Count);
            }
            else if (userEmail is null)
            {
                return (await GetPaginated(u => u.Id != userId, pageSize.Value, productPage.Value), users.Count);
            }
            else
            {
                users = await GetAll(c => c.Email == userEmail);
                return (await GetPaginated(u => u.Id != userId && u.Email.ToLower().Contains(userEmail.ToLower()),
                                                                                pageSize.Value, productPage.Value), users.Count);
            }
        }

        public async Task Delete(string id)
        {
            var userEntity = await _userRepository.Get(id);
            if (userEntity is null)
            {
                return;
            }

            await _userRepository.Delete(userEntity);
        }
    }
}
