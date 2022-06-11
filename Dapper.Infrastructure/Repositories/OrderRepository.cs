using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Core.Entities.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration configuration;
        public OrderRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //Add: registra UserId y agrega productos (ProductId)
        //Update: solo agrega o elimina productos (ProductId)
        public async Task<Order> AddUpdateAsync(OrderDto entity)
        {
            var addProducts = ListToDataTableProduct(entity.AddProducts);
            var deleteProducts = ListToDataTableProduct(entity.DeleteProducts);

            //TYPE_IC_PRODUCT: TYPE CREADO EN LA BD
            var p = new
            {
                Id = entity.Id,
                UserId = entity.UserId,
                AddProducts = addProducts.AsTableValuedParameter("TYPE_IC_PRODUCT"),
                DeleteProducts = deleteProducts.AsTableValuedParameter("TYPE_IC_PRODUCT")
            };

            var sql = "SP_IC_CREATE_UPDATE_ORDER";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                //var result = await connection.QueryAsync<Order>(sql, parameters, commandType: CommandType.StoredProcedure);
                var result = await connection.QuerySingleOrDefaultAsync<Order>(sql, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM ICOrders WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            var sql = "SELECT * FROM ICOrders";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Order>(sql);
                return result.ToList();
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var sqlOrder = "SELECT * FROM ICOrders WHERE Id = @Id";

            var sqlProducts = @"
                SELECT p.Id, p.Name, p.Description, p.Price, op.AddedOn, op.ModifiedOn
                FROM ICOrderProducts op
                INNER JOIN ICProducts p ON p.Id = op.ProductId
                WHERE op.OrderId = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var order = await connection.QuerySingleOrDefaultAsync<Order>(sqlOrder, new { Id = id });
                var products = await connection.QueryAsync<Product>(sqlProducts, order);
                order.Products =  products.ToList();
                return order;
            }
        }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(int userId)
        {
            var sqlOrders = "SP_IC_LIST_ORDERS_BY_USERID";

            var sqlProducts = @"
                SELECT p.Id, p.Name, p.Description, p.Price, op.AddedOn, op.ModifiedOn
                FROM ICOrderProducts op
                INNER JOIN ICProducts p ON p.Id = op.ProductId
                WHERE op.OrderId = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var orders = await connection.QueryAsync<Order>(sqlOrders, new { UserId = userId }, commandType: CommandType.StoredProcedure);
                foreach (var order in orders) {
                    var products = await connection.QueryAsync<Product>(sqlProducts, order);
                    order.Products = products.ToList();
                }
                return orders.ToList();
            }
        }

        private DataTable ListToDataTableProduct(List<int> list) {
            var dt = new DataTable();
            dt.Columns.Add("ProductId");
            DataRow dr = null;
            foreach (var id in list) {
                if (id > 0)
                {
                    dr = dt.NewRow();
                    dr["ProductId"] = id;
                    dt.Rows.Add(dr);
                    dr = null;
                }
            }
            return dt;
        }
    }
}
