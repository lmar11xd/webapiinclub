using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
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
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<User> AddAsync(User entity, string password)
        {
            //Encripta el Password 
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id);
            parameters.Add("@Username", entity.Username);
            parameters.Add("@PasswordHash", passwordHash, dbType: DbType.Binary);
            parameters.Add("@PasswordSalt", passwordSalt, dbType: DbType.Binary);

            var sql = "SP_IC_CREATE_UPDATE_USER";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        //Solo actualiza Username
        public async Task<User> UpdateAsync(User entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id);
            parameters.Add("@Username", entity.Username);
            parameters.Add("@PasswordHash", entity.PasswordHash, dbType: DbType.Binary);
            parameters.Add("@PasswordSalt", entity.PasswordSalt, dbType: DbType.Binary);

            var sql = "SP_IC_CREATE_UPDATE_USER";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM ICUsers WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM ICUsers";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM ICUsers WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                return result;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
