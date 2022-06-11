using Dapper.Core.Entities;
using Dapper.Core.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetByUserIdAsync(int userId);
        Task<IReadOnlyList<Order>> GetAllAsync();
        Task<Order> AddUpdateAsync(OrderDto entity);
        Task<int> DeleteAsync(int id);
    }
}
