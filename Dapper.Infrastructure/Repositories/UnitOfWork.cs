using Dapper.Application.Interfaces;

namespace Dapper.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUserRepository userRepository
        )
        {
            Products = productRepository;
            Orders = orderRepository;
            Users = userRepository;
        }

        public IProductRepository Products { get; }

        public IOrderRepository Orders { get; }

        public IUserRepository Users { get; }
    }
}
