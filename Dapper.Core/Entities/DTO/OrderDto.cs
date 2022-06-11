using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Core.Entities.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> AddProducts { get; set; }//Lista de Id´s de productos a agregar
        public List<int> DeleteProducts { get; set; }//Lista de Id´s de productos a eliminar
    }
}
