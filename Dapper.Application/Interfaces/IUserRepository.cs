using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User> AddAsync(User entity, string password);
        Task<User> UpdateAsync(User entity);
        Task<int> DeleteAsync(int id);
    }
}
